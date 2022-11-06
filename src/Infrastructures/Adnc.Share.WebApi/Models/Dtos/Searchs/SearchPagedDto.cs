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
    public abstract class SearchPagedDto : ISearchPagedDto
    {
        private int _pageIndex;
        private int _pageSize;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex < 1 ? 1 : _pageIndex;
            set => _pageIndex = value;
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize < 5) _pageSize = 5;
                if (_pageSize > 100) _pageSize = 100;
                return _pageSize;
            }
            set => _pageSize = value;
        }
    }
}
