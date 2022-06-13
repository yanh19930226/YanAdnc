using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Dtos
{
    public class OrderMarkCreatedStatusDto
    {
        public bool IsSuccess { get; set; }

        public string Remark { get; set; }
    }
}
