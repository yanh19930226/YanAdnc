using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Core.Microsoft.DependencyInjection;
using Adnc.Shared.WebApi.Authentication;
using Adnc.Shared.WebApi.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar : IDependencyRegistrar
    {
        public string Name => "webapi";
        protected IConfiguration Configuration { get; init; }
        protected IServiceCollection Services { get; init; }
        protected IServiceInfo ServiceInfo { get; init; }

        /// <summary>
        /// 服务注册与系统配置
        /// </summary>
        /// <param name="services"></param>
        protected AbstractWebApiDependencyRegistrar(IServiceCollection services)
        {
            Services = services;
            Configuration = services.GetConfiguration();
            ServiceInfo = services.GetServiceInfo();
        }

        /// <summary>
        /// 注册服务入口方法
        /// </summary>
        public abstract void AddAdnc();

        /// <summary>
        /// 注册Webapi通用的服务
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        protected virtual void AddWebApiDefault() =>
            AddWebApiDefault<BearerAuthenticationRemoteProcessor, PermissionRemoteHandler>();

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
            .AddMemoryCache();
            Configure();
            AddControllers();
            AddAuthentication<TAuthenticationProcessor>();
            AddAuthorization<TAuthorizationHandler>();
            AddCors();
            AddSwaggerGen();
            AddMiniProfiler();
            AddApplicationServices();
        }
    }
}
