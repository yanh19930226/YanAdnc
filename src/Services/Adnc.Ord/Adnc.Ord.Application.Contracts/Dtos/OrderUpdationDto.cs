using Adnc.Shared.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Dtos
{
    public class OrderUpdationDto : IDto
    {
        /// <summary>
        /// 收货信息
        /// </summary>
        public OrderReceiverDto DeliveryInfomaton { get; set; }
    }
}
