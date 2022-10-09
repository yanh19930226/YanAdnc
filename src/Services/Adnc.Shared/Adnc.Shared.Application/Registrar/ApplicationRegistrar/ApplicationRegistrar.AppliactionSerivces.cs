using Adnc.Infra.Caching.Interceptor.Castle;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.System.Extensions.Types;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Shared.Application.Interceptors;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Adnc.Shared.Application.Registrar;

public static partial class ApplicationRegistrar
{
    public static List<Type> DefaultInterceptorTypes => new() { typeof(OperateLogInterceptor), typeof(CachingInterceptor), typeof(UowInterceptor) };

    /// <summary>
    /// 注册Application服务
    /// </summary>
    public static IServiceCollection AddAppliactionSerivcesWithInterceptors(this IServiceCollection Services, IServiceInfo ServiceInfo,Action<IServiceCollection> action = null)
    {
        action?.Invoke(Services);

        var appServiceType = typeof(IAppService);

        var serviceTypes = ServiceInfo.StartAssembly.GetExportedTypes().Where(type => type.IsInterface && type.IsAssignableTo(appServiceType)).ToList();
        serviceTypes.ForEach(serviceType =>
        {
            var implType = ServiceInfo.StartAssembly.ExportedTypes.FirstOrDefault(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true));
            if (implType is null)
                return;

            Services.AddScoped(implType);
            Services.TryAddSingleton(new ProxyGenerator());
            Services.AddScoped(serviceType, provider =>
            {
                var interfaceToProxy = serviceType;
                var target = provider.GetService(implType);
                var interceptors = DefaultInterceptorTypes.ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor).ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();
                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(interfaceToProxy, target, interceptors);
                return proxy;
            });
        });

        return Services;
    }

    /// <summary>
    /// 注册Application的IHostedService服务
    /// </summary>
    public static IServiceCollection AddApplicaitonHostedServices(this IServiceCollection Services)
    {
        var serviceType = typeof(IHostedService);

        var implTypes = Assembly.GetExecutingAssembly().ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
        implTypes.ForEach(implType =>
        {
            Services.AddSingleton(serviceType, implType);
        });

        return Services;
    }
}