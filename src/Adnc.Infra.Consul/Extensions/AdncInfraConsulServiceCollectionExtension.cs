using Adnc.Infra.Consul.Discover;
using Adnc.Infra.Consul.Discover.Handler;
using Adnc.Infra.Consul.Registration;
using Adnc.Infra.Consul.TokenGenerator;
using Adnc.Infra.Core.Adnc.Configuration;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Adnc.Infra.Consul.Extensions
{
    public static class AdncInfraConsulServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraConsul(this IServiceCollection services, IConfigurationSection consulSection)
            => AddAdncInfraConsul(services, consulSection.Get<ConsulConfig>());

        public static IServiceCollection AddAdncInfraConsul(this IServiceCollection services, ConsulConfig consulConfig)
        {
            services.AddScoped<ITokenGenerator, DefaultTokenGenerator>();
            services.AddScoped<SimpleDiscoveryDelegatingHandler>();
            services.AddScoped<ConsulDiscoverDelegatingHandler>();
            services.AddSingleton<ConsulRegistration>();

            services.AddSingleton(x => new ConsulClient(x => x.Address = new Uri(consulConfig.ConsulUrl)));
            services.AddSingleton<IConsulServiceProvider, ConsulServiceProvider>();
            return services;
        }
    }
}
