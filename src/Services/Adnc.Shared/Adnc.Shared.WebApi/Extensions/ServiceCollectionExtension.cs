using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.System.Extensions.Types;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    /// <summary>
    ///  统一注册Adnc.WebApi通用服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupAssembly"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NullReferenceException"></exception>
    public static IServiceCollection AddAdnc(this IServiceCollection services, IServiceInfo serviceInfo)
    {
        if (serviceInfo?.StartAssembly is null)
            throw new ArgumentNullException(nameof(serviceInfo));

        #region 注册WebApi通用服务
        var webApiRegistarType = serviceInfo.StartAssembly.ExportedTypes.FirstOrDefault(m => m.IsAssignableTo(typeof(IWebApiRegistrar)) && m.IsNotAbstractClass(true));
        if (webApiRegistarType is null)
            throw new NullReferenceException(nameof(IWebApiRegistrar));

        var webapiRegistar = Activator.CreateInstance(webApiRegistarType, services) as IWebApiRegistrar;
        webapiRegistar?.AddAdncWebApi();

        #endregion

        #region 注册Appication通用服务
        var appicationRegistarType = serviceInfo.StartAssembly.ExportedTypes.FirstOrDefault(m => m.IsAssignableTo(typeof(IApplicationRegistrar)) && m.IsNotAbstractClass(true));
        if (appicationRegistarType is null)
            throw new NullReferenceException(nameof(IApplicationRegistrar));

        var appicationRegistar = Activator.CreateInstance(appicationRegistarType, services) as IApplicationRegistrar;
        appicationRegistar?.AddAdncApplication();
        #endregion

        return services;
    }
}