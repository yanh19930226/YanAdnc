using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.EfEnities.Config
{
    public struct EntityProperty
    {
        public readonly int MaxLength;

        public readonly bool IsRequired;

        private EntityProperty(bool isRequired, int maxLength)
        {
            IsRequired = isRequired;
            MaxLength = maxLength;
        }

        public static implicit operator EntityProperty((bool IsRequired, int MaxLength) info)
        {
            return new EntityProperty(info.IsRequired, info.MaxLength);
        }
    }
}
