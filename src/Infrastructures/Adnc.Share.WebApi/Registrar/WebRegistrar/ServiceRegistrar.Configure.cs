using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Adnc.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// 注册配置类到IOC容器
        /// </summary>
        public static IServiceCollection ConfigureConfig(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
        {
            Services
                .Configure<JwtConfig>(Configuration.GetSection(JwtConfig.Name))
                .Configure<ThreadPoolSettings>(Configuration.GetSection(ThreadPoolSettings.Name))
                .Configure<KestrelConfig>(Configuration.GetSection(KestrelConfig.Name));

            return Services;
        }
    }
}
