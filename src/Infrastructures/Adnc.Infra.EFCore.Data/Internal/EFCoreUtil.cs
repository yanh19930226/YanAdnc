using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EfCore.MySQL.Internal
{
    internal static class EFCoreUtil
    {
        internal static object[] GetEntityKeyValues<TEntity>(Func<TEntity, object>[] keyValueGetter, TEntity entity)
            => keyValueGetter.Select(x => x.Invoke(entity)).ToArray();
    }
}
