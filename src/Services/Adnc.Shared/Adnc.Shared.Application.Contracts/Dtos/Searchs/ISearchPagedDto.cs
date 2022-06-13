using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Dtos.Searchs
{
    /// <summary>
    /// 查询条件基类
    /// </summary>
    public interface ISearchPagedDto : IDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
