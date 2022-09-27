using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册跨域组件
        /// </summary>
        protected virtual void AddCors()
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
        }
    }

}
