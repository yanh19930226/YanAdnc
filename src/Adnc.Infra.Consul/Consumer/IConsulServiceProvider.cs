using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Consul.Consumer
{
    public interface IConsulServiceProvider
    {
        Task<IList<ServiceEntry>> GetAllServicesAsync(string serviceName);

        Task<IList<string>> GetHealthServicesAsync(string serviceName);
    }
}
