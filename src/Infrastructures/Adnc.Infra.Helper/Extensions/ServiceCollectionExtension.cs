using Adnc.Infra.Caching.Extensions;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.IdGenerater.Yitter;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAdncInfraYitterIdGenerater(this IServiceCollection services, IConfigurationSection redisSection)
    {
        if (services.HasRegistered(nameof(AddAdncInfraYitterIdGenerater)))
            return services;

        return services
            .AddAdncInfraCaching(redisSection)
            .AddSingleton<WorkerNode>()
            .AddHostedService<WorkerNodeHostedService>();
    }
}