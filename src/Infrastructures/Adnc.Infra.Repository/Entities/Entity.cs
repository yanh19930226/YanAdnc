using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities
{
    public class Entity : IEntity<long>, IConcurrency, ICreateAuditInfo, IModifyAuditInfo, ISoftDelete
    {
        public long Id { get; set; }
        public byte[] Version { get; set; }
        public bool IsDeleted { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
