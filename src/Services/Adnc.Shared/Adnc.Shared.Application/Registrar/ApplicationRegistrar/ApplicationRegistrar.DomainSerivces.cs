using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.System.Extensions.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adnc.Shared.Application.Registrar;

public static partial class ApplicationRegistrar
{
    /// <summary>
    /// 注册Domain服务
    /// </summary>
    public static IServiceCollection AddDomainSerivces<TDomainService>(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo,Action<IServiceCollection> action = null)
        where TDomainService : class
    {
        action?.Invoke(Services);

        var serviceType = typeof(TDomainService);
        var RepositoryOrDomainLayerAssembly = ServiceInfo.StartAssembly;
        var implTypes = RepositoryOrDomainLayerAssembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
        implTypes.ForEach(implType =>
        {
            Services.AddScoped(implType, implType);
        });

        return Services;
    }
}