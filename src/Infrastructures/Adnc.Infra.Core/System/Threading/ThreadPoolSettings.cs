using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.System.Threading
{
    public class ThreadPoolSettings
    {
        public const string Name = "ThreadPoolSettings";
        public int MinThreads { get; set; } = 300;
        public int MinCompletionPortThreads { get; set; } = 300;
        public int MaxThreads { get; set; } = 32767;
        public int MaxCompletionPortThreads { get; set; } = 1000;
    }
}
