using Adnc.Shared.Application.Contracts.Dtos;
using Adnc.Shared.Application.Contracts.Dtos.Searchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Application.Contracts.Vos
{
    [Serializable]
    public class PageModelDto<T> : IDto
    {
        private IReadOnlyList<T> _data = Array.Empty<T>();

        public PageModelDto()
        {
        }

        public PageModelDto(SearchPagedDto search)
            : this(search, default, default)
        {
        }

        public PageModelDto(SearchPagedDto search, IReadOnlyList<T> data, int count, dynamic xData = null)
            : this(search.PageIndex, search.PageSize, data, count)
        {
            XData = xData;
        }

        public PageModelDto(int pageIndex, int pageSize, IReadOnlyList<T> data, int count, dynamic xData = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            Data = data;
            XData = xData;
        }

        public IReadOnlyList<T> Data
        {
            get => _data;
            set => _data = value ?? Array.Empty<T>();
        }

        public int RowsCount => _data.Count;

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount => (RowsCount + PageSize - 1) / PageSize;

        public int TotalCount { get; set; }

        public dynamic XData { get; set; }
    }
}
