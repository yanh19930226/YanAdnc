using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Shared.WebApi.Authentication;
using Adnc.Shared.WebApi.Authentication.Basic;
using Adnc.Shared.WebApi.Authentication.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;


namespace Adnc.Shared.WebApi.Registrar
{
    public static partial class ServiceRegistrar
    {
        /// <summary>
        /// <summary>
        /// 注册身份认证组件
        /// </summary>
        public static IServiceCollection AddAuthentication<TAuthenticationHandler>(this IServiceCollection Services, IConfiguration Configuration, IServiceInfo ServiceInfo)
            where TAuthenticationHandler : AbstractAuthenticationProcessor
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Services
                .AddScoped<AbstractAuthenticationProcessor, TAuthenticationHandler>();
            Services
                .AddAuthentication(HybridDefaults.AuthenticationScheme)
                .AddHybrid()
                .AddBasic(options => options.Events.OnTokenValidated = (context) =>
                {
                    var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                    var claims = context.Principal.Claims;
                    userContext.Id = long.Parse(claims.First(x => x.Type == BasicDefaults.NameId).Value);
                    userContext.Account = claims.First(x => x.Type == BasicDefaults.UniqueName).Value;
                    userContext.Name = claims.First(x => x.Type == BasicDefaults.Name).Value;
                    userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    return Task.CompletedTask;
                })
                .AddBearer(options => options.Events.OnTokenValidated = (context) =>
                {
                    var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                    var claims = context.Principal.Claims;
                    userContext.Id = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
                    userContext.Account = claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                    userContext.Name = claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value;
                    userContext.RoleIds = claims.First(x => x.Type == "roleids").Value;
                    userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    return Task.CompletedTask;
                })
                //.AddJwtBearer(options =>
                //{
                //    var jwtConfig = Configuration.GetJWTSection().Get<JwtConfig>();
                //    options.TokenValidationParameters = JwtSecurityTokenHandlerExtension.GenarateTokenValidationParameters(jwtConfig);
                //    options.Events = JwtSecurityTokenHandlerExtension.GenarateJwtBearerEvents();
                //})
                ;

            return Services;
        }
    }
}
