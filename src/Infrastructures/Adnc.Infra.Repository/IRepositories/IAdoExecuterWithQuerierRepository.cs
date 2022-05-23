using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.IRepositories
{
    public interface IAdoExecuterWithQuerierRepository : IAdoExecuterRepository, IAdoQuerierRepository
    {
    }
}
