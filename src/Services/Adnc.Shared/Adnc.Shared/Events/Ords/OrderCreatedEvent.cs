using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Events.Ords
{
    /// <summary>
    /// 订单创建事件
    /// </summary>
    [Serializable]
    public class OrderCreatedEvent : EventEntity<OrderCreatedEvent.EventData>
    {
        public OrderCreatedEvent()
        {
        }

        public OrderCreatedEvent(long id, EventData eventData, string eventSource)
            : base(id, eventData, eventSource)
        {
        }

        public class EventData
        {
            public long OrderId { get; set; }

            public IEnumerable<OrderItem> Products { get; set; }
        }

        public class OrderItem
        {
            public long ProductId { get; set; }
            public int Qty { get; set; }
        }
    }
}
