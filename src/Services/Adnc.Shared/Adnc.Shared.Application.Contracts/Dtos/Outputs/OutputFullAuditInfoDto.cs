﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Outputs
{
    /// <summary>
    /// DTO基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Serializable]
    public abstract class OutputFullAuditInfoDto : OutputBaseAuditDto, IFullAuditInfo
    {
        /// <summary>
        /// 最后更新人
        /// </summary>
        public virtual long? ModifyBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
    }
}
