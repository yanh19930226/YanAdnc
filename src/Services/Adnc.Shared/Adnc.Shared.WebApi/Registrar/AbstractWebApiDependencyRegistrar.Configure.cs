using Adnc.Infra.Core.Adnc.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册配置类到IOC容器
        /// </summary>
        protected virtual void Configure()
        {
            Services
                .Configure<JwtConfig>(Configuration.GetSection(JwtConfig.Name))
                .Configure<ThreadPoolSettings>(Configuration.GetSection(ThreadPoolSettings.Name))
                .Configure<KestrelConfig>(Configuration.GetSection(KestrelConfig.Name));
        }
    }
}
