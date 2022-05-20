using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Consumer
{
    /// <summary>
    /// 服务器IP地址列表
    /// </summary>
    public interface ILoadBalancer
    {
        string Resolve(IList<string> services);
    }

    /// <summary>
    /// 随机算法
    /// </summary>
    public class RandomLoadBalancer : ILoadBalancer
    {
        private readonly Random _random = new Random();

        public string Resolve(IList<string> services)
        {
            var index = _random.Next(services.Count);
            return services[index];
        }
    }
    /// <summary>
    /// 循环算法
    /// </summary>
    public class RoundRobinLoadBalancer : ILoadBalancer
    {
        private readonly object _lock = new object();
        private int _index = 0;

        public string Resolve(IList<string> services)
        {
            lock (_lock)
            {
                if (_index >= services.Count)
                {
                    _index = 0;
                }
                return services[_index++];
            }
        }
    }

    /// <summary>
    /// 创建算法
    /// </summary>
    public static class TypeLoadBalancer
    {
        public static ILoadBalancer RandomLoad = new RandomLoadBalancer();
        public static ILoadBalancer RoundRobinLoad = new RoundRobinLoadBalancer();
    }
}
