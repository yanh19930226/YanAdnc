using Adnc.Infra.Consul.Configuration;
using Adnc.Infra.Core.Adnc.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Extensions
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder AddConsulConfiguration(this IConfigurationBuilder configurationBuilder, ConsulConfig config, bool reloadOnChanges = false)
        {
            return configurationBuilder.Add(new DefaultConsulConfigurationSource(config, reloadOnChanges));
        }
    }
}
