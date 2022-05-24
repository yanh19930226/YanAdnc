using Adnc.Infra.Consul.Registration;
using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.System.Extensions.Collection;
using Adnc.Infra.Core.System.Extensions.String;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void RegisterToConsul(this IApplicationBuilder app)
        {
            var kestrelConfig = app.ApplicationServices.GetRequiredService<IOptions<KestrelConfig>>()?.Value;
            if (kestrelConfig is null)
                throw new NotImplementedException(nameof(kestrelConfig));

            var registration = app.ApplicationServices.GetRequiredService<ConsulRegistration>();
            var ipAddresses = registration.GetLocalIpAddress("InterNetwork");
            if (ipAddresses.IsNullOrEmpty())
                throw new NotImplementedException(nameof(kestrelConfig));

            var defaultEnpoint = kestrelConfig.Endpoints.FirstOrDefault(x => x.Key.EqualsIgnoreCase("default")).Value;
            if (defaultEnpoint is null || defaultEnpoint.Url.IsNullOrWhiteSpace())
                throw new NotImplementedException(nameof(kestrelConfig));

            var serviceAddress = new Uri(defaultEnpoint.Url);
            if (serviceAddress.Host == "0.0.0.0")
                serviceAddress = new Uri($"{serviceAddress.Scheme}://{ipAddresses.FirstOrDefault()}:{serviceAddress.Port}");

            registration.Register(serviceAddress);
        }

        public static void RegisterToConsul(this IApplicationBuilder app, Uri serviceAddress)
        {
            if (serviceAddress is null)
                throw new ArgumentNullException(nameof(serviceAddress));

            var registration = app.ApplicationServices.GetRequiredService<ConsulRegistration>();
            registration.Register(serviceAddress);
        }

        public static void RegisterToConsul(this IApplicationBuilder app, AgentServiceRegistration instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            var registration = app.ApplicationServices.GetRequiredService<ConsulRegistration>();
            registration.Register(instance);
        }

        public static void RegisterToConsulIfProduction(this IApplicationBuilder app)
        {
            var environment = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
            if (environment.IsProduction())
                RegisterToConsul(app);
        }
    }
}
