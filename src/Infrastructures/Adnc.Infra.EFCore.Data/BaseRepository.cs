using Adnc.Infra.Core.System.Extensions.Collection;
using Adnc.Infra.Core.System.Extensions.Expressions;
using Adnc.Infra.EFCore.Data;
using Adnc.Infra.Repository.Entities;
using Adnc.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Adnc.Infra.EfCore.MySQL
{

    public class BaseRepository<TEntity> : AbstractBaseRepository<DbContext, TEntity>, IBaseRepository<TEntity>
           where TEntity : Entity, IEntity<long>
    {
        private readonly IAdoQuerierRepository? _adoQuerier;

        public BaseRepository(DbContext dbContext, IAdoQuerierRepository? adoQuerier = null)
            : base(dbContext)
            => _adoQuerier = adoQuerier;

        public IAdoQuerierRepository? AdoQuerier
        {
            get
            {
                if (_adoQuerier is null)
                    return null;
                if (!_adoQuerier.HasDbConnection())
                    _adoQuerier.ChangeOrSetDbConnection(DbContext.Database.GetDbConnection());
                return _adoQuerier;
            }
        }
    }
}
