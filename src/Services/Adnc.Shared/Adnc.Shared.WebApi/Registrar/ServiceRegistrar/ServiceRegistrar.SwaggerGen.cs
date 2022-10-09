using Adnc.Infra.Core.Adnc.Interfaces;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// 注册swagger组件
        /// </summary>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
        {
            var openApiInfo = new OpenApiInfo { Title = ServiceInfo.ShortName, Version = ServiceInfo.Version };
            //Services.AddEndpointsApiExplorer();
            Services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(openApiInfo.Version, openApiInfo);

                    // 采用bearer token认证
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                    });
                    //设置全局认证
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                    });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{ServiceInfo.StartAssembly.GetName().Name}.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{ServiceInfo.StartAssembly.GetName().Name.Replace("WebApi", "Application.Contracts")}.xml"));
                })
                .AddFluentValidationRulesToSwagger();

            return Services;
        }
    }
}
