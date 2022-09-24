using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.EfCore.MySQL;
using Adnc.Infra.EfCore.MySQL.Transaction;
using Adnc.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Adnc.Infra.EFCore.Data.MySql
{
    public static class AdncEfCoreMySqlServiceCollectionExtension
    {
        public static IServiceCollection AddAdncInfraEfCoreMySql(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            if (services.HasRegistered(nameof(AddAdncInfraEfCoreMySql)))
                return services;

            services.AddDbContext<DbContext, MySqlDbContext>(optionsBuilder);
            services.TryAddScoped<IUnitOfWork, MySqlUnitOfWork<MySqlDbContext>>();
            services.TryAddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
