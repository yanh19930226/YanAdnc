using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Diagnostics
{
    public class BeforeGetRequestEventData : EventData
    {
        public BeforeGetRequestEventData(string cacheType, string name, string operation, string[] cacheKeys, System.TimeSpan? expiration = null)
            : base(cacheType, name, operation)
        {
            this.CacheKeys = cacheKeys;
            this.Expiration = expiration;
        }

        public string[] CacheKeys { get; set; }

        public TimeSpan? Expiration { get; set; }
    }
}
