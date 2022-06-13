using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Outputs
{
    [Serializable]
    public abstract class OutputBaseAuditDto : OutputDto, IBasicAuditInfo
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual long? CreateBy { get; set; }

        /// <summary>
        /// 创建时间/注册时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
    }
}
