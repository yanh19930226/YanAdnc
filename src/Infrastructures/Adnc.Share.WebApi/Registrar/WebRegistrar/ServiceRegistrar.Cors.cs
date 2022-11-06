using Adnc.Infra.Core.Adnc.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// 注册跨域组件
        /// </summary>
        public static IServiceCollection AddCors(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
        {
            Services.AddCors(options =>
            {
                var corsHosts = Configuration.GetValue("CorsHosts", string.Empty).Split(",", StringSplitOptions.RemoveEmptyEntries);
                options.AddPolicy(ServiceInfo.CorsPolicy, policy =>
                {
                    policy
                    .WithOrigins(corsHosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            return Services;
        }
    }

}
