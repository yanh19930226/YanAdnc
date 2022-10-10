using Adnc.Infra.Caching.Extensions;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.System.Extensions.Types;
using Adnc.Shared.Application.BloomFilter;
using Adnc.Shared.Application.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adnc.Shared.Application.Registrar;

public static partial class ApplicationRegistrar
{
    /// <summary>
    /// 注册Caching相关处理服务
    /// </summary>
    /// <param name="builder"></param>
    public static IServiceCollection AddCaching(this IServiceCollection Services,IConfigurationSection RedisSection, IServiceInfo ServiceInfo,Action<IServiceCollection> action = null)
    {
        action?.Invoke(Services);

        //if(this.IsEnableSkyApm())
        //{
        //    SkyApm.AddCaching();
        //}

        Services.AddAdncInfraCaching(RedisSection);
        var ApplicationLayerAssembly = ServiceInfo.StartAssembly;
        var serviceType = typeof(ICachePreheatable);
        var implTypes = ApplicationLayerAssembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
        if (implTypes.IsNotNullOrEmpty())
        {
            implTypes.ForEach(implType =>
            {
                Services.AddSingleton(implType, implType);
                Services.AddSingleton(x => x.GetRequiredService(implType) as ICachePreheatable);
            });
        }

        return Services;
    }

    /// <summary>
    /// 注册BloomFilter相关处理服务
    /// </summary>
    /// <param name="builder"></param>
    public static IServiceCollection AddBloomFilters(this IServiceCollection Services, IServiceInfo ServiceInfo,Action<IServiceCollection> action = null)
    {
        action?.Invoke(Services);
        var serviceType = typeof(IBloomFilter);
        var ApplicationLayerAssembly = ServiceInfo.StartAssembly;
        var implTypes = ApplicationLayerAssembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
        if (implTypes.IsNotNullOrEmpty())
            implTypes.ForEach(implType => Services.AddSingleton(serviceType, implType));

        return Services;
    }
}