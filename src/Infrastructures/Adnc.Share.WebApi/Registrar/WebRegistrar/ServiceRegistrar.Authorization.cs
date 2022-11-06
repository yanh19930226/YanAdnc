using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Shared.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// 注册授权组件
        /// PermissionHandlerRemote 跨服务授权
        /// PermissionHandlerLocal  本地授权,adnc.usr走本地授权，其他服务走Rpc授权
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        public static IServiceCollection AddAuthorization<TAuthorizationHandler>(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
            where TAuthorizationHandler : AbstractPermissionHandler
        {
            Services
                .AddScoped<IAuthorizationHandler, TAuthorizationHandler>();
            Services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(AuthorizePolicy.Default, policy =>
                    {
                        policy.Requirements.Add(new PermissionRequirement());
                    });
                });

            return Services;
        }
    }
}
