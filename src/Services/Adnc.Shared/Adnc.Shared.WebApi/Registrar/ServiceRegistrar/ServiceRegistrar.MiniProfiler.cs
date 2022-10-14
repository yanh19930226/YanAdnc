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
        /// 注册 MiniProfiler 组件
        /// </summary>
        public static IServiceCollection AddMiniProfiler(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo) {
            Services
                .AddMiniProfiler(
                options => 
                       //options.RouteBasePath = $"/{ServiceInfo.ShortName}/profiler
                       options.RouteBasePath = "/profiler"
                )
                .AddEntityFramework();
            return Services;
        }
    }
}
