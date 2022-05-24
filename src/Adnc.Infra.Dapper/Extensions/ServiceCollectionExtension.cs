using Adnc.Infra.Dapper.Repositories;
using Adnc.Infra.Repository.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Dapper.Extensions
{
    public static class AdncInfraDapperServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraDapper(this IServiceCollection services)
        {
            services.TryAddScoped<IAdoExecuterWithQuerierRepository, DapperRepository>();
            services.TryAddScoped<IAdoExecuterRepository, DapperRepository>();
            services.TryAddScoped<IAdoQuerierRepository, DapperRepository>();
            return services;
        }
    }
}
