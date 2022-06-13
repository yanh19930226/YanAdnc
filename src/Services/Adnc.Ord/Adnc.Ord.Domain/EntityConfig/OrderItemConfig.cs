using Adnc.Infra.Repository.Entities.EfEnities.Config;
using Adnc.Ord.Domain.Aggregates.OrderAggregate;
using Adnc.Shared.Consts.EntityConsts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Ord.Domain.EntityConfig
{
    public class OrderItemConfig : EntityTypeConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.OrderId).IsRequired();

            builder.OwnsOne(x => x.Product, y =>
            {
                y.Property(z => z.Id).IsRequired().HasColumnName("producid");
                y.Property(z => z.Name).IsRequired().HasMaxLength(OrderItemConsts.Name_MaxLength).HasColumnName("productname");
                y.Property(z => z.Price).IsRequired().HasColumnName("productprice").HasColumnType("decimal(18,4)");
            });

            builder.Property(x => x.Count).IsRequired();
        }
    }
}
