using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Diagnostics
{
    public class BeforeSetRequestEventData : EventData
    {
        public BeforeSetRequestEventData(string cacheType, string name, string operation, IDictionary<string, object> dict, System.TimeSpan expiration)
            : base(cacheType, name, operation)
        {
            this.Dict = dict;
            this.Expiration = expiration;
        }

        public IDictionary<string, object> Dict { get; set; }

        public TimeSpan Expiration { get; set; }
    }
}
