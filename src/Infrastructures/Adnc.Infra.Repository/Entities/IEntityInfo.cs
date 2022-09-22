using Adnc.Infra.Repository.IRepositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.Entities
{
    public interface IEntityInfo
    {
        Operater GetOperater();

        void OnModelCreating(dynamic modelBuilder);
    }
}
