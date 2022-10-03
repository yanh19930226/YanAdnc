using Adnc.Shared;
using Adnc.Shared.Repository.EfEntities;
using System.Reflection;

namespace Adnc.Usr.Entities;

public class EntityInfo : AbstracSharedEntityInfo
{
    public EntityInfo(UserContext userContext) : base(userContext)
    {
    }

    protected override Assembly GetCurrentAssembly() => GetType().Assembly;
}