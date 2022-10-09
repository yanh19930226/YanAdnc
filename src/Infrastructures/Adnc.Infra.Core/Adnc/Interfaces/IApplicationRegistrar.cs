using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Interfaces
{
    public interface IApplicationRegistrar
    {
        public IConfigurationSection RedisSection { get; init; }
        public IConfigurationSection MysqlSection { get; init; }
        public IConfigurationSection MongoDbSection { get; init; }
        public IConfigurationSection ConsulSection { get; init; }
        public IConfigurationSection RabbitMqSection { get; init; }

        public void AddAdncApplication();
    }
}
