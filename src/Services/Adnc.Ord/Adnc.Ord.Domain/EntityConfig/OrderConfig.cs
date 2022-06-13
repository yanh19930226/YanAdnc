﻿using Adnc.Infra.Repository.Entities.EfEnities.Config;
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
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CustomerId);

            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");

            builder.Property(x => x.Remark).HasMaxLength(OrdConsts.Remark_MaxLength);

            builder.OwnsOne(x => x.Status, y =>
            {
                y.Property(z => z.Code).HasColumnName("statuscode");
                y.Property(z => z.ChangesReason).HasColumnName("statuschangesreason").HasMaxLength(OrdConsts.ChangesReason_MaxLength);
            });

            builder.OwnsOne(x => x.Receiver, y =>
            {
                y.Property(z => z.Name).HasColumnName("receivername").HasMaxLength(OrdConsts.Name_MaxLength);
                y.Property(z => z.Phone).HasColumnName("receiverphone").HasMaxLength(OrdConsts.Phone_MaxLength);
                y.Property(z => z.Address).HasColumnName("receiveraddress").HasMaxLength(OrdConsts.Address_MaxLength);
            });

            builder.HasMany(x => x.Items).WithOne().HasForeignKey(y => y.OrderId);
        }
    }
}
