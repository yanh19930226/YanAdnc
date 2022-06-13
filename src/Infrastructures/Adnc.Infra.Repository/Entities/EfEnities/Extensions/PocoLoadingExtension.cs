using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.EfEnities.Extensions
{
    // <summary>
    /// 延时加载扩展方法
    /// </summary>
    public static class PocoLoadingExtension
    {
        public static TRelated Load<TRelated>(
             this Action<object, string> loader,
             object entity,
             ref TRelated navigationField,
             [CallerMemberName] string navigationName = null)
             where TRelated : class
        {
            loader?.Invoke(entity, navigationName);

            return navigationField;
        }
    }
}
