using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Interfaces
{
    public enum TrackerKind
    {
        Null = 0x01,
        Db = 0x02,
        Redis = 0x04
    }

    public interface IMessageTracker
    {
        TrackerKind Kind { get; }
        Task<bool> HasProcessedAsync(long eventId, string trackerName);
        Task MarkAsProcessedAsync(long eventId, string trackerName);
    }
}
