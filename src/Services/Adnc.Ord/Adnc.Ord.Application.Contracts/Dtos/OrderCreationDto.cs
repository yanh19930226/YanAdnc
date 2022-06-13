using Adnc.Shared.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Application.Contracts.Dtos
{
    public class OrderCreationDto : IDto
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 收货信息
        /// </summary>
        public OrderReceiverDto DeliveryInfomaton { get; set; }

        /// <summary>
        /// 订单子项
        /// </summary>
        public virtual ICollection<OrderCreationItemDto> Items { get; set; }

        public class OrderCreationItemDto : IDto
        {
            public long ProductId { get; set; }

            public int Count { get; set; }
        }
    }
}
