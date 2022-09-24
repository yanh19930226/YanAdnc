using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Diagnostics
{
    public class BeforeRemoveRequestEventData : EventData
    {
        public BeforeRemoveRequestEventData(string cacheType, string name, string operation, string[] cacheKeys)
            : base(cacheType, name, operation)
        {
            this.CacheKeys = cacheKeys;
        }

        public string[] CacheKeys { get; set; }
    }
}
