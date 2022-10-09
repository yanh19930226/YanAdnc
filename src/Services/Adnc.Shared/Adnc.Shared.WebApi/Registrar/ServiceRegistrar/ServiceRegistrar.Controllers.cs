using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.Adnc.Json;
using Adnc.Infra.Core.System.Extensions.DataTimes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;


namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// Controllers 注册
        /// Sytem.Text.Json 配置
        /// FluentValidation 注册
        /// ApiBehaviorOptions 配置
        /// </summary>
        public static IServiceCollection AddControllers(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
        {
            Services
                .AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                    options.JsonSerializerOptions.Encoder = SystemTextJson.GetAdncDefaultEncoder();
                //该值指示是否允许、不允许或跳过注释。
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                //dynamic与匿名类型序列化设置
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //dynamic
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                //匿名类型
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddFluentValidation(cfg =>
                {
                //Continue 验证失败，继续验证其他项
                ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
                //cfg.ValidatorOptions.DefaultClassLevelCascadeMode = FluentValidation.CascadeMode.Continue;
                // Optionally set validator factory if you have problems with scope resolve inside validators.
                // cfg.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            });

            Services
                .Configure<ApiBehaviorOptions>(options =>
                {
                //调整参数验证返回信息格式
                //关闭自动验证
                //options.SuppressModelStateInvalidFilter = true;
                //格式化验证信息
                options.InvalidModelStateResponseFactory = (context) =>
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Detail = context.ModelState.GetValidationSummary("<br>"),
                            Title = "参数错误",
                            Status = (int)HttpStatusCode.BadRequest,
                            Type = "https://httpstatuses.com/400",
                            Instance = context.HttpContext.Request.Path
                        };

                        return new ObjectResult(problemDetails)
                        {
                            StatusCode = problemDetails.Status
                        };
                    };
                });
            //add skyamp
            //_services.AddSkyApmExtensions().AddCaching();

            return Services;
        }
    }
}
