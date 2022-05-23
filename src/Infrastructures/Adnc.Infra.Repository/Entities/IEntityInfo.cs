using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities
{
    public interface IEntityInfo
    {
        IEnumerable<EntityTypeInfo> GetEntitiesTypeInfo();
    }

    public class EntityTypeInfo
    {
        public Type? Type { get; set; }

        public IEnumerable<object>? DataSeeding { get; set; }
    }
}
