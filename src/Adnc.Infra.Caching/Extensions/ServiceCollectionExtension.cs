using Adnc.Infra.Caching.Configurations;
using Adnc.Infra.Caching.Core.Interceptor;
using Adnc.Infra.Caching.Core.Internal;
using Adnc.Infra.Caching.Core.Serialization;
using Adnc.Infra.Caching.Interceptor.Castle;
using Adnc.Infra.Caching.StackExchange;
using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.Core.System.Extensions.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adnc.Infra.Caching.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraCaching(this IServiceCollection services, IConfigurationSection redisSection)
        {
            if (services.HasRegistered(nameof(AddAdncInfraCaching)))
                return services;

            services
                .Configure<RedisOptions>(redisSection)
                .Configure<RedisConfig>(redisSection)
                .AddSingleton<ICachingKeyGenerator, DefaultCachingKeyGenerator>()
                .AddScoped<CachingInterceptor>()
                .AddScoped<CachingAsyncInterceptor>();
            var serviceType = typeof(IRedisSerializer);
            var implementations = serviceType.Assembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
            implementations.ForEach(implementationType => services.AddSingleton(serviceType, implementationType));

            var redisConfig = redisSection.Get<RedisConfig>();
            switch (redisConfig.Provider)
            {
                case RedisConstValue.Provider.StackExchange:
                    AddAdncStackExchange(services);
                    break;
                case RedisConstValue.Provider.ServiceStack:
                    break;
                case RedisConstValue.Provider.FreeRedis:
                    break;
                case RedisConstValue.Provider.CSRedis:
                    break;
                default:
                    throw new NotSupportedException(nameof(redisConfig.Provider));
            }
            return services;
        }

        public static IServiceCollection AddAdncStackExchange(IServiceCollection services)
        {
            return
                services
                .AddSingleton<DefaultDatabaseProvider>()
                .AddSingleton<DefaultRedisProvider>()
                .AddSingleton<IRedisProvider>(x => x.GetRequiredService<DefaultRedisProvider>())
                .AddSingleton<IDistributedLocker>(x => x.GetRequiredService<DefaultRedisProvider>())
                .AddSingleton<ICacheProvider, CachingProvider>();
        }
    }
}
