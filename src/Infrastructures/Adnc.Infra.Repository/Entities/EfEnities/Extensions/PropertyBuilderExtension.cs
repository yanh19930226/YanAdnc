using Adnc.Infra.Repository.Entities.EfEnities.Config;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities.EfEnities.Extensions
{
    public static class PropertyBuilderExtension
    {
        public static PropertyBuilder<string> IsRequiredAndMaxLength(this PropertyBuilder<string> builder, EntityProperty entConst)
        {
            builder.IsRequired(entConst.IsRequired);
            builder.HasMaxLength(entConst.MaxLength);
            return builder;
        }
    }
}
