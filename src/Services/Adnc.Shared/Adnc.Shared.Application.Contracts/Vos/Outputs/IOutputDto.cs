using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Outputs
{
    /// <summary>
    /// OutputDto基类
    /// </summary>
    public interface IOutputDto : IDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
    }
}
