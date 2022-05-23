using Adnc.Infra.Core.Adnc.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Configuration
{
    public class DefaultConsulConfigurationSource : IConfigurationSource
    {
        private readonly ConsulConfig _config;
        private readonly bool _reloadOnChanges;

        public DefaultConsulConfigurationSource(ConsulConfig config, bool reloadOnChanges)
        {
            _config = config;
            _reloadOnChanges = reloadOnChanges;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DefaultConsulConfigurationProvider(_config, _reloadOnChanges);
        }
    }
}
