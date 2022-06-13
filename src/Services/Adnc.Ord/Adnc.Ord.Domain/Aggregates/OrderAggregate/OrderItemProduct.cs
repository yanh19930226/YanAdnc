﻿using Adnc.Infra.Core.Adnc.Guard;
using Adnc.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Domain.Aggregates.OrderAggregate
{
    public record OrderItemProduct : ValueObject
    {
        public long Id { get; }

        public string Name { get; }

        public decimal Price { get; }

        private OrderItemProduct()
        {
        }

        public OrderItemProduct(long id, string name, decimal price)
        {
            this.Id = Guard.Checker.GTZero(id, nameof(id));
            this.Name = Guard.Checker.NotNullOrEmpty(name, nameof(name));
            this.Price = Guard.Checker.GTZero(price, nameof(price));
        }
    }
}
