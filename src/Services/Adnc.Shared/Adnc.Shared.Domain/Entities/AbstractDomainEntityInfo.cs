using Adnc.Infra.Repository.Entities.EfEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Domain.Entities
{
    public abstract class AbstractDomainEntityInfo : AbstractEntityInfo
    {
        protected override IEnumerable<Type> GetEntityTypes(Assembly assembly)
        {
            var efEntities = assembly.GetTypes().Where(m =>
                                                       m.FullName != null
                                                       && (typeof(AggregateRoot).IsAssignableFrom(m) || typeof(DomainEntity).IsAssignableFrom(m))
                                                       && !m.IsAbstract).ToArray();

            return efEntities;
        }
    }
}
