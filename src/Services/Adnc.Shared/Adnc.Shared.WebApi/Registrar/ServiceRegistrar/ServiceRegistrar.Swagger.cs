using Adnc.Infra.Core.Adnc.Interfaces;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.Generation;
using NSwag.AspNetCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// 设置Span为可编辑
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        public static void SetSpanEditable(this SwaggerUi3Settings swaggerUIOptions)
        {
            StringBuilder stringBuilder = new StringBuilder(swaggerUIOptions.CustomHeadContent);
            stringBuilder.Append(@"
                    <script type='text/javascript'>
                    window.addEventListener('load', function () {
                        setTimeout(() => {
                            let createElement = window.ui.React.createElement
                            ui.React.createElement = function () {
                                let array = Array.from(arguments)
                                if (array.length == 3) {
                                    if (array[0] == 'span' && !array[1]) {
                                        array[1] = { contentEditable: true }
                                    }
                                }

                                let ele = createElement(...array)
                                return ele
                            }
                        })
                    })
                    </script>");
            swaggerUIOptions.CustomHeadContent = stringBuilder.ToString();
        }

        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IServiceInfo ServiceInfo)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(option =>
            {
                // 可选，为true时API返回支持的版本信息
                option.ReportApiVersions = true;
                // 不提供版本时，默认为1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                // 请求中未指定版本时默认为1.0
                option.DefaultApiVersion = new ApiVersion(1, 0);
                //option.ApiVersionReader = new HeaderApiVersionReader("apiVersion");
                option.ApiVersionReader = new QueryStringApiVersionReader("apiVersion");

            }).AddVersionedApiExplorer(option =>
            {
                // 版本名的格式：v+版本号
                option.GroupNameFormat = "'v'V";
                //属性是为了标记当客户端没有指定版本号的时候是否使用默认版本号
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            //获取webapi版本信息，用于swagger多版本支持 
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            //遍历版本信息给不同版本添加NSwag支持，如果只写一个就只有一份
            foreach (var description in provider.ApiVersionDescriptions)
            {
                //添加NSwag
                services.AddOpenApiDocument(settings =>
                {
                    settings.DocumentName = description.GroupName;
                    settings.Version = description.GroupName;
                    settings.Title = ServiceInfo.ServiceName;
                    settings.Description = ServiceInfo.Description;
                    settings.ApiGroupNames = new string[] { description.GroupName };
                    settings.UseControllerSummaryAsTagDescription = true;

                    settings.AddSecurity("身份认证Token", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme()
                    {
                        Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                        Name = "Authorization",
                        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                        Type = NSwag.OpenApiSecuritySchemeType.ApiKey
                    });
                }).AddFluentValidationRulesToSwagger();
            }

            return services;
        }
    }

    public class MySchemaProcessor : ISchemaProcessor
    {
        static readonly ConcurrentDictionary<Type, Tuple<string, object>[]> dict = new ConcurrentDictionary<Type, Tuple<string, object>[]>();
        public void Process(SchemaProcessorContext context)
        {
            var schema = context.Schema;
            if (context.Type.IsEnum)
            {
                var items = GetTextValueItems(context.Type);
                if (items.Length > 0)
                {
                    string decription = string.Join(",", items.Select(f => $"{f.Item1}={f.Item2}"));
                    schema.Description = string.IsNullOrEmpty(schema.Description) ? decription : $"{schema.Description}:{decription}";
                }
            }
            else if (context.Type.IsClass && context.Type != typeof(string))
            {
                UpdateSchemaDescription(schema);
            }
        }
        private void UpdateSchemaDescription(NJsonSchema.JsonSchema schema)
        {
            if (schema.HasReference)
            {
                var s = schema.ActualSchema;
                if (s != null && s.Enumeration != null && s.Enumeration.Count > 0)
                {
                    if (!string.IsNullOrEmpty(s.Description))
                    {
                        string description = $"【{s.Description}】";
                        if (string.IsNullOrEmpty(schema.Description) || !schema.Description.EndsWith(description))
                        {
                            schema.Description += description;
                        }
                    }
                }
            }

            foreach (var key in schema.Properties.Keys)
            {
                var s = schema.Properties[key];
                UpdateSchemaDescription(s);
            }
        }
        /// <summary>
        /// 获取枚举值+描述  
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private Tuple<string, object>[] GetTextValueItems(Type enumType)
        {
            Tuple<string, object>[] tuples;
            if (dict.TryGetValue(enumType, out tuples) && tuples != null)
            {
                return tuples;
            }

            FieldInfo[] fields = enumType.GetFields();
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    string key = attribute?.Description ?? field.Name;
                    int value = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null));
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    list.Add(new KeyValuePair<string, int>(key, value));
                }
            }
            tuples = list.OrderBy(f => f.Value).Select(f => new Tuple<string, object>(f.Key, f.Value.ToString())).ToArray();
            dict.TryAdd(enumType, tuples);
            return tuples;
        }
    }

    public class NullToEmptyStringResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberSerialization">序列化成员</param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties().Select(c =>
            {
                var jsonProperty = base.CreateProperty(c, memberSerialization);
                jsonProperty.ValueProvider = new NullToEmptyStringValueProvider(c);
                return jsonProperty;
            }).ToList();
        }
    }

    public class NullToEmptyStringValueProvider : IValueProvider
    {
        private readonly PropertyInfo _memberInfo;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            var result = _memberInfo.GetValue(target);

            if (_memberInfo.PropertyType == typeof(string) && result == null)

                result = string.Empty;
            return result;
        }

        /// <summary>
        /// 设置Value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}
