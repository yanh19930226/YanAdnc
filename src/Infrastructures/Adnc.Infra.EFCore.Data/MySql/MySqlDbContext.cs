using Adnc.Infra.EfCore.MySQL;
using Adnc.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EFCore.Data.MySql
{
    public class MySqlDbContext : AdncDbContext
    {
        public MySqlDbContext(
            DbContextOptions options,
            IEntityInfo entityInfo)
            : base(options, entityInfo)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //System.Diagnostics.Debugger.Launch();
            modelBuilder.HasCharSet("utf8mb4 ");
            base.OnModelCreating(modelBuilder);
        }
    }
}
