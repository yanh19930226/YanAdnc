using Adnc.Infra.Core.DependencyInjection;
using Adnc.Infra.EventBus;
using Adnc.Infra.Helper;
using Adnc.Infra.Repository.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Share.WebApi.Models.Entities
{
    public abstract class AggregateRoot : DomainEntity, IConcurrency, IEntity<long>
    {
        public byte[] RowVersion { get; set; }

        public Lazy<IEventPublisher> EventPublisher => new(() =>
        {
            var httpContext = InfraHelper.Accessor.GetCurrentHttpContext();
            if (httpContext is not null)
                return httpContext.RequestServices.GetRequiredService<IEventPublisher>();
            if (ServiceLocator.Provider is not null)
                return ServiceLocator.Provider.GetRequiredService<IEventPublisher>();
            throw new NotImplementedException(nameof(IEventPublisher));
        });
    }
}
