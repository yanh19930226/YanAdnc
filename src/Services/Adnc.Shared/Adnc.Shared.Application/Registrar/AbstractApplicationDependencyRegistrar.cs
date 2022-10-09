using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.Dapper.Extensions;
using Adnc.Infra.Mapper.Extensions;
using Adnc.Shared.Application.Caching;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Shared.Application.Interceptors;
using Adnc.Shared.Application.Services.Trackers;
using Adnc.Shared.Rpc;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Adnc.Shared.Application.Registrar;

public abstract partial class AbstractApplicationDependencyRegistrar : IApplicationRegistrar
{
    public string Name => "application";
    public abstract Assembly ApplicationLayerAssembly { get; }
    public abstract Assembly ContractsLayerAssembly { get; }
    public abstract Assembly RepositoryOrDomainLayerAssembly { get; }

    //protected SkyApmExtensions SkyApm { get; init; }
    protected List<AddressNode> RpcAddressInfo { get; init; }
    protected IServiceCollection Services { get; init; }
    protected IConfiguration Configuration { get; init; }
    protected IServiceInfo ServiceInfo { get; init; }
    protected IConfigurationSection RedisSection { get; init; }
    protected IConfigurationSection MysqlSection { get; init; }
    protected IConfigurationSection MongoDbSection { get; init; }
    protected IConfigurationSection ConsulSection { get; init; }
    protected IConfigurationSection RabbitMqSection { get; init; }

    protected AbstractApplicationDependencyRegistrar(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentException("IServiceCollection is null.");
        Configuration = services.GetConfiguration() ?? throw new ArgumentException("Configuration is null.");
        ServiceInfo = services.GetServiceInfo() ?? throw new ArgumentException("ServiceInfo is null.");
        RedisSection = Configuration.GetSection(RedisConfig.Name);
        MongoDbSection = Configuration.GetSection(MongoConfig.Name);
        MysqlSection = Configuration.GetSection(MysqlConfig.Name);
        ConsulSection = Configuration.GetSection(ConsulConfig.Name);
        RabbitMqSection = Configuration.GetSection(RabbitMqConfig.Name);
        //SkyApm = Services.AddSkyApmExtensions();
        RpcAddressInfo = Configuration.GetSection(Rpc.AddressNode.Name).Get<List<Rpc.AddressNode>>();
    }

    /// <summary>
    /// 注册所有服务
    /// </summary>
    public abstract void AddAdncApplication();

    /// <summary>
    /// 注册adnc.application通用服务
    /// </summary>
    protected virtual void AddApplicaitonDefault()
    {
        Services
            .AddValidatorsFromAssembly(ContractsLayerAssembly, ServiceLifetime.Scoped)
            .AddAdncInfraAutoMapper(ApplicationLayerAssembly)
            //.AddAdncInfraYitterIdGenerater(RedisSection)
            //.AddAdncInfraConsul(ConsulSection)
            .AddAdncInfraDapper()
        //.AddAppliactionSerivcesWithInterceptors()
        //.AddApplicaitonHostedServices()
        .AddEfCoreContextWithRepositories(MysqlSection,ServiceInfo,this.IsDevelopment())
        .AddMongoContextWithRepositries(MongoDbSection)
        .AddCaching(RedisSection, ServiceInfo)
        .AddBloomFilters();

        AddApplicationSharedServices();
    }

    /// <summary>
    /// 注册application.shared层服务
    /// </summary>
    protected void AddApplicationSharedServices()
    {
        Services.AddSingleton(typeof(Lazy<>));
        Services.AddScoped<UserContext>();
        Services.AddScoped<OperateLogInterceptor>();
        Services.AddScoped<OperateLogAsyncInterceptor>();
        Services.AddScoped<UowInterceptor>();
        Services.AddScoped<UowAsyncInterceptor>();
        //Services.AddSingleton<IBloomFilter, NullBloomFilter>();
        //Services.AddSingleton<BloomFilterFactory>();
        //Services.AddHostedService<BloomFilterHostedService>();
        Services.AddHostedService<CachingHostedService>();
        Services.AddHostedService<Channels.ChannelConsumersHostedService>();
        Services.AddScoped<IMessageTracker, DbMessageTrackerService>();
        Services.AddScoped<IMessageTracker, RedisMessageTrackerService>();
        Services.AddScoped<MessageTrackerFactory>();
    }
}