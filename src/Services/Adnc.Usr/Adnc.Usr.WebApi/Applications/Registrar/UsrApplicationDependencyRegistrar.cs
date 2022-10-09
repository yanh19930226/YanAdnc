using Adnc.Shared.Application.Registrar;
using Adnc.Usr.Application.Contracts.Services;
using Adnc.Usr.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Adnc.Usr.Application.Registrar;

public sealed class UsrApplicationDependencyRegistrar : AbstractApplicationDependencyRegistrar
{
    public override Assembly ApplicationLayerAssembly => Assembly.GetExecutingAssembly();

    public override Assembly ContractsLayerAssembly => Assembly.GetExecutingAssembly();

    public override Assembly RepositoryOrDomainLayerAssembly => Assembly.GetExecutingAssembly();

    //public override Assembly ContractsLayerAssembly => typeof(IUserAppService).Assembly;

    //public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityInfo).Assembly;

    public UsrApplicationDependencyRegistrar(IServiceCollection services) : base(services)
    {
    }

    public override void AddAdnc() => AddApplicaitonDefault();
}