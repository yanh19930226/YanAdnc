using Adnc.Shared.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Dtos
{
    public class OrderReceiverDto : IDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
