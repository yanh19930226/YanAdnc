﻿using Adnc.Infra.Caching.Extensions;
using Adnc.Infra.Core.System.Extensions.Types;
using Adnc.Shared.Application.Caching;
using Adnc.Shared.Application.Caching.SkyApm;
using Microsoft.Extensions.DependencyInjection;

namespace Adnc.Shared.Application.Registrar;

public abstract partial class AbstractApplicationDependencyRegistrar
{
    /// <summary>
    /// 注册Caching相关处理服务
    /// </summary>
    /// <param name="builder"></param>
    protected virtual void AddCaching(Action<IServiceCollection> action = null)
    {
        action?.Invoke(Services);
        //if(this.IsEnableSkyApm())
        //{
        //    SkyApm.AddCaching();
        //}
        Services.AddAdncInfraCaching(RedisSection);
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
    }

    ///// <summary>
    ///// 注册BloomFilter相关处理服务
    ///// </summary>
    ///// <param name="builder"></param>
    //protected virtual void AddBloomFilters(Action<IServiceCollection> action = null)
    //{
    //    action?.Invoke(Services);

    //    var serviceType = typeof(IBloomFilter);
    //    var implTypes = ApplicationLayerAssembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
    //    if (implTypes.IsNotNullOrEmpty())
    //        implTypes.ForEach(implType => Services.AddSingleton(serviceType, implType));
    //}
}