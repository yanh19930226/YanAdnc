﻿ using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Shared.Consts.RegistrationCenter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

public static class HostExtension
{
    /// <summary>
    /// register to (consul/nacos/clusterip...)
    /// </summary>
    public static IHost UseRegistrationCenter(this IHost host)
    {
        var configuration = host.Services.GetService<IConfiguration>();
        var serviceInfo = host.Services.GetService<IServiceInfo>();
        var registeredType = configuration.GetRegisteredType().ToLower();
        switch (registeredType)
        {
            case RegisteredTypeConsts.Consul:
                host.RegisterToConsul(serviceInfo.Id);
                break;
            case RegisteredTypeConsts.Nacos:
                // TODO
                //app.RegisterToNacos(serviceInfo.Id);
                break;
            default:
                break;
        }
        return host;
    }
}