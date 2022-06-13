using Adnc.Infra.Core.Adnc.Guard;
using Adnc.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Domain.Aggregates.OrderAggregate
{
    public record OrderReceiver : ValueObject
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; }

        public OrderReceiver(string name, string phone, string address)
        {
            this.Name = Guard.Checker.NotNullOrEmpty(name, nameof(name));
            this.Phone = Guard.Checker.NotNullOrEmpty(phone, nameof(phone));
            this.Address = Guard.Checker.NotNullOrEmpty(address, nameof(address));
        }
    }
}
