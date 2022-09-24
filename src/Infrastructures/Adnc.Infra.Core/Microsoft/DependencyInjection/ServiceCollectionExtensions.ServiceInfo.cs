using Adnc.Infra.Core.Adnc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Microsoft.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceInfo GetServiceInfo(this IServiceCollection services)
        {
            return services.GetSingletonInstance<IServiceInfo>();
        }
    }
}
