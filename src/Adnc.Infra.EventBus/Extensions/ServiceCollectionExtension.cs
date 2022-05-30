using Adnc.Infra.EventBus.Cap;
using Adnc.Infra.EventBus.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EventBus.Extensions
{
    public static class AdncInfraEventBusServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraEventBus(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMqProducer>();
            services.AddSingleton<IEventPublisher, CapPublisher>();
            return services;
        }
    }
}
