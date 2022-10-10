using Adnc.Shared.WebApi.Registrar;
using Adnc.Usr.WebApi.Authentication;
using Adnc.Usr.WebApi.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Adnc.Usr.WebApi.Registrar;

public  class UsrWebApiDependencyRegistrar : AbstractWebApiDependencyRegistrar
{
    public UsrWebApiDependencyRegistrar(IServiceCollection services)
        : base(services)
    {
    }

    public UsrWebApiDependencyRegistrar(IApplicationBuilder app)
        : base(app)
    {
    }

    public override void AddAdncWebApi()
    {
        AddWebApiDefault<BearerAuthenticationLocalProcessor, PermissionLocalHandler>();
        //AddHealthChecks(true, true, true, false);
        //Services.AddGrpc();
    }

    public override void AddAdncApplication()
    {
        AddApplicaitonDefault();
    }

    public override void UseAdnc()
    {
        UseWebApiDefault(endpointRoute: endpoint =>
        {
            //endpoint.MapGrpcService<Grpc.AuthGrpcServer>();
            //endpoint.MapGrpcService<Grpc.UsrGrpcServer>();
        });
    }
}