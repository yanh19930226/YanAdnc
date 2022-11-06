using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Outputs
{
    [Serializable]
    public abstract class OutputDto : IOutputDto, IModifyAuditInfoDto, ICreateAuditInfoDto
    {
        public virtual long Id { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public virtual long? ModifyBy { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
        /// <summary>
        /// CreateBy
        /// </summary>
        public long? CreateBy { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
