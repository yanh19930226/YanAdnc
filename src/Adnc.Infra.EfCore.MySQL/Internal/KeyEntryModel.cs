using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EfCore.MySQL.Internal
{
    internal class KeyEntryModel
    {
        public string PropertyName { get; set; }

        public string ColumnName { get; set; }

        public object Value { get; set; }
    }
}
