using Adnc.Infra.Core.Adnc.Interfaces;
using Adnc.Infra.Helper;
using Adnc.Infra.IdGenerater.Yitter;
using Adnc.Infra.Repository.IRepositories;
using Adnc.Shared.Repository.EfEntities;

namespace Adnc.Shared.Application.Services.Trackers;

public class DbMessageTrackerService : IMessageTracker
{
    public IBaseRepository<EventTracker> _trackerRepo;

    public DbMessageTrackerService(IBaseRepository<EventTracker> trackerRepo)
    {
        _trackerRepo = trackerRepo;
    }

    public TrackerKind Kind => TrackerKind.Db;

    public async Task<bool> HasProcessedAsync(long eventId, string trackerName)
    {
        return await _trackerRepo.AnyAsync(x => x.EventId == eventId && x.TrackerName == trackerName, true);
    }

    public async Task MarkAsProcessedAsync(long eventId, string trackerName)
    {
        await _trackerRepo.InsertAsync(new EventTracker
        {
            Id = IdGenerater.GetNextId(),
            EventId = eventId,
            TrackerName = trackerName
        });
    }
}
