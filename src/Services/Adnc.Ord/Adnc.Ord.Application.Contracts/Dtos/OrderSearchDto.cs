using Adnc.Shared.Application.Contracts.Dtos.Searchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Dtos
{
    public class OrderSearchPagedDto : SearchPagedDto
    {
        public long? Id { get; set; }
    }
}
