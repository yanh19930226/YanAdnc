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