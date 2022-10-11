using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.DependencyInjection;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Infra.Dapper.Extensions;
using Adnc.Infra.Mapper.Extensions;
using Adnc.Shared.Application.BloomFilter;
using Adnc.Shared.Application.Caching;
using Adnc.Shared.Application.Channels;
using Adnc.Shared.Application.Contracts.Interfaces;
using Adnc.Shared.Application.Interceptors;
using Adnc.Shared.Application.Registrar;
using Adnc.Shared.Application.Services.Trackers;
using Adnc.Shared.Rpc;
using Adnc.Shared.WebApi.Authentication;
using Adnc.Shared.WebApi.Authorization;
using Adnc.Shared.WebApi.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar : IWebApiRegistrar, IApplicationRegistrar,IMiddlewareRegistrar
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
        public readonly IApplicationBuilder App;

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

        /// <summary>
        /// 中间件注册方法
        /// </summary>
        /// <param name="app"></param>
        public AbstractWebApiDependencyRegistrar(IApplicationBuilder app)
        {
            App = app;
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
            .AddBloomFilters(ServiceInfo);
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
            Services.AddSingleton<IBloomFilter, NullBloomFilter>();
            Services.AddSingleton<BloomFilterFactory>();
            Services.AddHostedService<BloomFilterHostedService>();
            Services.AddHostedService<CachingHostedService>();
            Services.AddHostedService<ChannelConsumersHostedService>();
            Services.AddScoped<IMessageTracker, DbMessageTrackerService>();
            Services.AddScoped<IMessageTracker, RedisMessageTrackerService>();
            Services.AddScoped<MessageTrackerFactory>();
        }

        #endregion

        #region 中间件入口方法
        /// <summary>
        /// 注册中间件入口方法
        /// </summary>
        /// <param name="app"></param>
        public abstract void UseAdnc();

        /// <summary>
        /// 注册webapi通用中间件
        /// </summary>
        protected virtual void UseWebApiDefault(
            Action<IApplicationBuilder> beforeAuthentication = null,
            Action<IApplicationBuilder> afterAuthentication = null,
            Action<IApplicationBuilder> afterAuthorization = null,
            Action<IEndpointRouteBuilder> endpointRoute = null)
        {
            ServiceLocator.Provider = App.ApplicationServices;
            var environment = App.ApplicationServices.GetService<IHostEnvironment>();
            var serviceInfo = App.ApplicationServices.GetService<IServiceInfo>();
            //var consulOptions = App.ApplicationServices.GetService<IOptions<ConsulConfig>>();

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            App
                .UseDefaultFiles(defaultFilesOptions)
                .UseStaticFiles()
                .UseCustomExceptionHandler()
                .UseRealIp(x => x.HeaderKey = "X-Forwarded-For")
                .UseCors(serviceInfo.CorsPolicy);

            if (environment.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                App.UseMiniProfiler();
            }

            App
                .UseSwagger(c =>
                {
                    c.RouteTemplate = $"/{serviceInfo.ShortName}/swagger/{{documentName}}/swagger.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"/", Description = serviceInfo.Description } };
                    });
                })
                .UseSwaggerUI(c =>
                {
                    var assembly = serviceInfo.StartAssembly;
                    c.IndexStream = () => assembly.GetManifestResourceStream($"{assembly.GetName().Name}.swagger_miniprofiler.html");
                    c.SwaggerEndpoint($"/{serviceInfo.ShortName}/swagger/{serviceInfo.Version}/swagger.json", $"{serviceInfo.ServiceName}-{serviceInfo.Version}");
                    c.RoutePrefix = $"{serviceInfo.ShortName}";
                })
                //.UseHealthChecks($"/{consulOptions.Value.HealthCheckUrl}", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //// 该响应输出是一个json，包含所有检查项的详细检查结果
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //})
                .UseRouting();

            //.UseHttpMetrics();
            //DotNetRuntimeStatsBuilder
            //.Customize()
            //.WithContentionStats()
            //.WithGcStats()
            //.WithThreadPoolStats()
            //.StartCollecting();

            beforeAuthentication?.Invoke(App);
            App.UseAuthentication();
            afterAuthentication?.Invoke(App);
            App.UseAuthorization();
            afterAuthorization?.Invoke(App);

            App
                .UseEndpoints(endpoints =>
                {
                    endpointRoute?.Invoke(endpoints);

                    //endpoints.MapMetrics();
                    //endpoints.MapControllers();
                    endpoints.MapControllers().RequireAuthorization();
                });
        } 
        #endregion
    }
}
