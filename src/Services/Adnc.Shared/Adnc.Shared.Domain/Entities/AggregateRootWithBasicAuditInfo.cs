using Adnc.Infra.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Domain.Entities
{
    public class AggregateRootWithBasicAuditInfo : AggregateRoot, IBasicAuditInfo
    {
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
