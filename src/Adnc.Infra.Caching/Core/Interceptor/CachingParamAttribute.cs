using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Interceptor
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CachingParamAttribute : Attribute
    {
        public CachingParamAttribute()
        {
        }
    }
}
