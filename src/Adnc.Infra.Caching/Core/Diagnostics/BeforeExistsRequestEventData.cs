using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Diagnostics
{
    public class BeforeExistsRequestEventData : EventData
    {
        public BeforeExistsRequestEventData(string cacheType, string name, string operation, string cacheKey)
            : base(cacheType, name, operation)
        {
            this.CacheKey = cacheKey;
        }

        public string CacheKey { get; set; }
    }
}
