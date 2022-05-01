using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.EfEnities
{
    public abstract class EfEntity : Entity, IEfEntity<long>
    {
    }
}
