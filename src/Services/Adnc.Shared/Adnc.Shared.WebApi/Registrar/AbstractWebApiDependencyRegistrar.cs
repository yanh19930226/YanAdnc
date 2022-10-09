using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.Dapper.Extensions;
using Adnc.Infra.Mapper.Extensions;
using Adnc.Shared.Application.Caching;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Shared.Application.Interceptors;
using Adnc.Shared.Application.Registrar;
using Adnc.Shared.Application.Services.Trackers;
using Adnc.Shared.Rpc;
using Adnc.Shared.WebApi.Authentication;
using Adnc.Shared.WebApi.Authorization;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar : IWebApiRegistrar,IApplicationRegistrar
    {
        public string Name => "webapi";
        public IConfiguration Configuration { get; init; }
        public IServiceCollection Services { get; init; }
        public IServiceInfo ServiceInfo { get; init; }
        public List<AddressNode> RpcAddressInfo { get; init; }
        public IConfigurationSection RedisSection { get; init; }
        public IConfigurationSection MysqlSection { get; init; }
        public IConfigurationSection MongoDbSection { get; init; }
        public IConfigurationSection ConsulSection { get; init; }
        public IConfigurationSection RabbitMqSection { get; init; }

        /// <summary>
        /// 服务注册与系统配置
        /// </summary>
        /// <param name="services"></param>
        public AbstractWebApiDependencyRegistrar(IServiceCollection services)
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

        #region 注册服务Web入口方法
        /// <summary>
        /// 注册服务Web入口方法
        /// </summary>
        public abstract void AddAdncWebApi();

        /// <summary>
        /// 注册Webapi通用的服务
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        protected virtual void AddWebApiDefault() => AddWebApiDefault<BearerAuthenticationRemoteProcessor, PermissionRemoteHandler>();

        /// <summary>
        /// 注册Webapi通用的服务
        /// </summary>
        /// <typeparam name="TAuthenticationProcessor"><see cref="AbstractAuthenticationProcessor"/></typeparam>
        /// <typeparam name="TAuthorizationHandler"><see cref="AbstractPermissionHandler"/></typeparam>
        protected virtual void AddWebApiDefault<TAuthenticationProcessor, TAuthorizationHandler>()
            where TAuthenticationProcessor : AbstractAuthenticationProcessor
            where TAuthorizationHandler : AbstractPermissionHandler
        {
            Services
            .AddHttpContextAccessor()
            .AddMemoryCache()
            .ConfigureConfig(Configuration, ServiceInfo)
            .AddControllers(Configuration, ServiceInfo)
            .AddAuthentication<TAuthenticationProcessor>(Configuration, ServiceInfo)
            .AddAuthorization<TAuthorizationHandler>(Configuration, ServiceInfo)
            .AddCors(Configuration, ServiceInfo)
            .AddSwaggerGen(Configuration, ServiceInfo)
            .AddMiniProfiler(Configuration, ServiceInfo);
        } 
        #endregion

        #region 注册服务Application入口方法
        /// <summary>
        /// 注册服务Application入口方法
        /// </summary>
        public abstract void AddAdncApplication();

        /// <summary>
        /// 注册adnc.application通用服务
        /// </summary>
        protected virtual void AddApplicaitonDefault()
        {
            Services
                .AddValidatorsFromAssembly(ServiceInfo.StartAssembly, ServiceLifetime.Scoped)
                .AddAdncInfraAutoMapper(ServiceInfo.StartAssembly)
                //.AddAdncInfraYitterIdGenerater(RedisSection)
                //.AddAdncInfraConsul(ConsulSection)
                .AddAdncInfraDapper()
            .AddAppliactionSerivcesWithInterceptors(ServiceInfo)
            .AddApplicaitonHostedServices()
            .AddEfCoreContextWithRepositories(MysqlSection, ServiceInfo, this.IsDevelopment())
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
            //Services.AddHostedService<System.Threading.Channels.ChannelConsumersHostedService>();
            Services.AddScoped<IMessageTracker, DbMessageTrackerService>();
            Services.AddScoped<IMessageTracker, RedisMessageTrackerService>();
            Services.AddScoped<MessageTrackerFactory>();
        } 
        #endregion

    }
}
