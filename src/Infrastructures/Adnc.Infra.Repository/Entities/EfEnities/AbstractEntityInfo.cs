using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.EfEnities
{
    public abstract class AbstractEntityInfo : IEntityInfo
    {
        protected virtual IEnumerable<Type> GetEntityTypes(Assembly assembly)
        {
            var efEntities = assembly.GetTypes().Where(m =>
                                                       m.FullName != null
                                                       && typeof(EfEntity).IsAssignableFrom(m)
                                                       && !m.IsAbstract).ToArray();

            return efEntities;
        }

        public abstract IEnumerable<EntityTypeInfo> GetEntitiesTypeInfo();
    }
}
