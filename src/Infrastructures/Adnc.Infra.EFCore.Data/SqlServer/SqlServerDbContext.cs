using Adnc.Infra.EfCore.MySQL;
using Adnc.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EFCore.Data.SqlServer
{
    public class SqlServerDbContext : AdncDbContext
    {
        public SqlServerDbContext(
            DbContextOptions options,
            IEntityInfo entityInfo)
            : base(options, entityInfo)
        {
        }
    }
}
