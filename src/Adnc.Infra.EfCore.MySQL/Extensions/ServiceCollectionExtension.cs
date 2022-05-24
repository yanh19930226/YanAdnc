using Adnc.Infra.EfCore.MySQL.Repositories;
using Adnc.Infra.EfCore.MySQL.Transaction;
using Adnc.Infra.Repository.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EfCore.MySQL.Extensions
{
    public static class AdncEfCoreMySqlServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraEfCoreMySql(this IServiceCollection services)
        {
            services.TryAddScoped<UnitOfWorkStatus>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork<AdncDbContext>>();
            services.TryAddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            services.TryAddScoped(typeof(IEfBasicRepository<>), typeof(EfBasicRepository<>));
            return services;
        }
    }
}
