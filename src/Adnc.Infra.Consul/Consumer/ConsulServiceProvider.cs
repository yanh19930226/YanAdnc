using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Consumer
{
    public class ConsulServiceProvider : IConsulServiceProvider
    {
        //ConsulClient
        private readonly ConsulClient _consulClient;

        /// <summary>
        ///构造函数获取Consul地址
        /// </summary>
        /// <param name="uri"></param>
        public ConsulServiceProvider(string uri)
        {
            _consulClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(uri);
            });
        }
        /// <summary>
        /// 构造函数获取Consul地址
        /// </summary>
        /// <param name="uri"></param>
        public ConsulServiceProvider(Uri uri)
        {
            _consulClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = uri;
            });
        }

        /// <summary>
        /// 获取某给服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<IList<ServiceEntry>> GetAllServicesAsync(string serviceName)
        {
            var queryResult = await _consulClient.Health.Service(serviceName, string.Empty, false);
            return queryResult.Response;
        }

        /// <summary>
        /// 获取某个服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetHealthServicesAsync(string serviceName)
        {
            var queryResult = await _consulClient.Health.Service(serviceName, string.Empty, true);
            var result = new List<string>();
            foreach (var item in queryResult.Response)
            {
                result.Add($"{item.Service.Address}:{item.Service.Port}");
            }
            return result;
        }
    }
}
