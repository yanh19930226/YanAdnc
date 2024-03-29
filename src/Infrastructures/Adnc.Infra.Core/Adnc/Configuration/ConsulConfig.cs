﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Configuration
{
    public class ConsulConfig
    {
        public const string Name = "Consul";
        public string ConsulUrl { get; set; }
        public string ServiceName { get; set; }
        public string HealthCheckUrl { get; set; }
        public int HealthCheckIntervalInSecond { get; set; }
        public string[] ServerTags { get; set; }
        public string ConsulKeyPath { get; set; }
        public int DeregisterCriticalServiceAfter { get; set; }
        public int Timeout { get; set; }
    }
}
