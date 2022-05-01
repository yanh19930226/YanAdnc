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
        (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo();
    }
}
