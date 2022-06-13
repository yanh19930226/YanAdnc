using Adnc.Infra.Repository.Entities;
using Adnc.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Domain.EntityConfig
{
    public class EntityInfo : AbstractDomainEntityInfo
    {
        public override IEnumerable<EntityTypeInfo> GetEntitiesTypeInfo()
        {
            return base.GetEntityTypes(this.GetType().Assembly).Select(x => new EntityTypeInfo() { Type = x, DataSeeding = default });
        }
    }
}
