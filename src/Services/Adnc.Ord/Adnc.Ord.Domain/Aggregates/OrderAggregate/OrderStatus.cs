using Adnc.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Domain.Aggregates.OrderAggregate
{
    public record OrderStatus : ValueObject
    {
        public OrderStatusCodes Code { get; }

        public string? ChangesReason { get; }

        private OrderStatus()
        {
        }

        public OrderStatus(OrderStatusCodes statusCode, string? reason = null)
        {
            Code = statusCode;
            ChangesReason = reason is null ? string.Empty : reason.Trim();
        }
    }
}
