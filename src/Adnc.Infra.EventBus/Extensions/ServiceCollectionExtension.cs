using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.EventBus.Cap;
using Adnc.Infra.EventBus.Cap.Filters;
using Adnc.Infra.EventBus.RabbitMq;
using DotNetCore.CAP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Adnc.Infra.EventBus.Extensions
{
    public static class AdncInfraEventBusServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraCap<TSubscriber>(this IServiceCollection services, Action<CapOptions> setupAction)
        where TSubscriber : class, ICapSubscribe
        {
            if (services.HasRegistered(nameof(AddAdncInfraCap)))
                return services;
            services
                .AddSingleton<IEventPublisher, CapPublisher>()
                .AddScoped<TSubscriber>()
                .AddCap(setupAction)
                .AddSubscribeFilter<DefaultCapFilter>();
            return services;
        }

        public static IServiceCollection AddAdncInfraRabbitMq(this IServiceCollection services, IConfigurationSection rabitmqSection)
        {
            if (services.HasRegistered(nameof(AddAdncInfraRabbitMq)))
                return services;

            return services
                 .Configure<RabbitMqConfig>(rabitmqSection)
                 .AddSingleton<IRabbitMqConnection>(provider =>
                 {
                     var options = provider.GetRequiredService<IOptions<RabbitMqConfig>>();
                     var logger = provider.GetRequiredService<ILogger<RabbitMqConnection>>();
                     var serviceInfo = services.GetServiceInfo();
                     var clientProvidedName = serviceInfo?.Id ?? "unkonow";
                     return RabbitMqConnection.GetInstance(options, clientProvidedName, logger);
                 })
                 .AddSingleton<RabbitMqProducer>();
        }
    }
}
