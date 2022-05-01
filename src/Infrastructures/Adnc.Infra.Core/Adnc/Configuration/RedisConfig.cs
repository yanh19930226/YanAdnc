using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Configuration
{
    /// <summary>
    /// RedisConfig配置
    /// </summary>
    public class RedisConfig
    {
        public int MaxRdSecond { get; set; }
        public bool EnableLogging { get; set; }
        public int LockMs { get; set; }
        public int SleepMs { get; set; }
        public Dbconfig dbconfig { get; set; }
    }

    public class Dbconfig
    {
        public string ConnectionString { get; set; }
        public bool ReadOnly { get; set; }
    }
}
