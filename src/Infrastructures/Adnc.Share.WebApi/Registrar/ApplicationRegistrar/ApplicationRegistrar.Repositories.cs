using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.System.Extensions.Types;
using Adnc.Infra.Mongo;
using Adnc.Infra.Mongo.Configuration;
using Adnc.Infra.Mongo.Extensions;
using Adnc.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.EFCore.Data.MySql;

namespace Adnc.Shared.Application.Registrar;

public static partial class ApplicationRegistrar
{
    /// <summary>
    /// 注册EFCoreContext与仓储
    /// </summary>
    public static IServiceCollection AddEfCoreContextWithRepositories(this IServiceCollection Services, IConfigurationSection MysqlSection, IServiceInfo ServiceInfo, bool IsDevelopment)
    {
        var serviceType = typeof(IEntityInfo);

        var RepositoryOrDomainLayerAssembly = ServiceInfo.StartAssembly;

        var implType = RepositoryOrDomainLayerAssembly.ExportedTypes.FirstOrDefault(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true));
        if (implType is null)
            throw new NotImplementedException(nameof(IEntityInfo));
        else
            Services.AddScoped(serviceType, implType);

        Services.AddEfCoreContext(MysqlSection,ServiceInfo, IsDevelopment);

        return Services;
    }

    /// <summary>
    /// 注册EFCoreContext
    /// </summary>
    public static IServiceCollection AddEfCoreContext(this IServiceCollection Services, IConfigurationSection MysqlSection, IServiceInfo ServiceInfo, bool IsDevelopment)
    {
        var mysqlConfig = MysqlSection.Get<MysqlConfig>();
        var serverVersion = new MariaDbServerVersion(new Version(10, 5, 4));
        Services.AddAdncInfraEfCoreMySql(options =>
        {
            options.UseLowerCaseNamingConvention();
            options.UseMySql(mysqlConfig.ConnectionString, serverVersion, optionsBuilder =>
            {
                optionsBuilder.MinBatchSize(4)
                                        .MigrationsAssembly(ServiceInfo.StartAssembly.GetName().Name.Replace("WebApi", "Migrations"))
                                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            if (IsDevelopment)
            {
                //options.AddInterceptors(new DefaultDbCommandInterceptor())
                options.LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors();
            }
            //替换默认查询sql生成器,如果通过mycat中间件实现读写分离需要替换默认SQL工厂。
            //options.ReplaceService<IQuerySqlGeneratorFactory, AdncMySqlQuerySqlGeneratorFactory>();
        });

        return Services;
    }

    /// <summary>
    /// 注册MongoContext与仓储
    /// </summary>
    public static IServiceCollection AddMongoContextWithRepositries(this IServiceCollection Services, IConfigurationSection MongoDbSection, Action<IServiceCollection> action = null)
    {
        action?.Invoke(Services);

        var mongoConfig = MongoDbSection.Get<MongoConfig>();
        Services.AddAdncInfraMongo<MongoContext>(options =>
        {
            options.ConnectionString = mongoConfig.ConnectionString;
            options.PluralizeCollectionNames = mongoConfig.PluralizeCollectionNames;
            options.CollectionNamingConvention = (NamingConvention)mongoConfig.CollectionNamingConvention;
        });

        return Services;
    }
}