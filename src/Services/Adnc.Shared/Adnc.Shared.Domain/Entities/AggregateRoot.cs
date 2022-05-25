using Adnc.Infra.Repository.Entities;
using Adnc.Infra.Repository.Entities.EfEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Domain.Entities
{
    public abstract class AggregateRoot : DomainEntity, IConcurrency, IEfEntity<long>
    {
        public byte[] RowVersion { get; set; }

        public Lazy<IEventPublisher> EventPublisher => new(() => HttpContextHelper.GetCurrentHttpContext().RequestServices.GetRequiredService<IEventPublisher>());
    }
}
