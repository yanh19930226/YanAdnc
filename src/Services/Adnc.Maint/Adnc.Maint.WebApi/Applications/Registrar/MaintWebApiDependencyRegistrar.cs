using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Shared.Application.Registrar;
using Adnc.Shared.Rpc;
using Adnc.Shared.Rpc.Rest.Services;

namespace Adnc.Maint.WebApi.Registrar;

public sealed class MaintWebApiDependencyRegistrar : AbstractWebApiDependencyRegistrar
{
    public MaintWebApiDependencyRegistrar(IServiceCollection services) 
        : base(services)
    {
    }

    public MaintWebApiDependencyRegistrar(IApplicationBuilder app)
    : base(app)
    {
    }

    public override void AddAdncWebApi()
    {
        AddWebApiDefault();
        //AddHealthChecks(true, true, true, false);
        //Services.AddGrpc();
    }

    public override void AddAdncApplication()
    {

        //var RabbitMqSection = Configuration.GetSection(RabbitMqConfig.Name);
        //var RpcAddressInfo = Configuration.GetSection(AddressNode.Name).Get<List<AddressNode>>();

        AddApplicaitonDefault();
        //rpc-rest
        var restPolicies = this.GenerateDefaultRefitPolicies();
        Services.AddRestClient<IAuthRestClient>(Configuration, RpcAddressInfo,RpcConsts.UsrService, restPolicies);
        Services.AddRestClient<IUsrRestClient>(Configuration, RpcAddressInfo,RpcConsts.UsrService, restPolicies);
        Services.AddRabbitMqClient(RabbitMqSection);
    }

    public override void UseAdnc()
    {
         UseWebApiDefault(endpointRoute: endpoint =>
        {
            //endpoint.MapGrpcService<Grpc.MaintGrpcServer>();
        });
    }
}