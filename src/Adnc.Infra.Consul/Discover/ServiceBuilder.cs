using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Discover
{
    internal class ServiceBuilder : IServiceBuilder
    {
        public IConsulServiceProvider ServiceProvider { get; init; }
        public string ServiceName { get; set; }
        public string UriScheme { get; set; }
        public ILoadBalancer LoadBalancer { get; set; }

        public ServiceBuilder(IConsulServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
    }

    public interface IServiceBuilder
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        IConsulServiceProvider ServiceProvider { get; init; }

        /// <summary>
        /// 服务名称
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Uri方案
        /// </summary>
        string UriScheme { get; set; }

        /// <summary>
        /// 使用哪种策略
        /// </summary>
        ILoadBalancer LoadBalancer { get; set; }
    }
}
