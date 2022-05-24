using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Configuration
{
    public class KestrelConfig
    {
        public IDictionary<string, Endpoint> Endpoints { get; set; }

        public KestrelConfig() => Endpoints = new Dictionary<string, Endpoint>();

        public class Endpoint
        {
            public Endpoint()
            {
                if (string.IsNullOrWhiteSpace(Protocols))
                    Protocols = "Http1AndHttp2";
            }

            public string Url { get; set; }

            public string Protocols { get; set; }
        }
    }
}
