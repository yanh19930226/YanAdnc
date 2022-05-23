using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.IRepositories
{
    public interface IAdoRepository
    {
        void ChangeOrSetDbConnection(IDbConnection dbConnection);

        void ChangeOrSetDbConnection(string connectionString, DbTypes dbType);

        bool HasDbConnection();
    }
}
