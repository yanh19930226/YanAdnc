using Adnc.Infra.Core.Adnc.Guard;
using Adnc.Infra.Core.System.Extensions.Collection;
using Adnc.Infra.Core.System.Extensions.Types;
using Adnc.Infra.EfCore.MySQL.Transaction;
using Adnc.Infra.Repository.Entities;
using Adnc.Infra.Repository.IRepositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Adnc.Infra.EfCore.MySQL
{
    public abstract class AdncDbContext : DbContext
    {
        private readonly IEntityInfo _entityInfo;

        protected AdncDbContext(
            DbContextOptions options, 
            IEntityInfo entityInfo
            )
            : base(options)
        {
            _entityInfo = entityInfo;
            Database.AutoTransactionsEnabled = false;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = SetAuditFields();

            //没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
            var isManualTransaction = false;
            if (!Database.AutoTransactionsEnabled && Database.CurrentTransaction is null && changedEntities > 1)
            {
                isManualTransaction = true;
                Database.AutoTransactionsEnabled = true;
            }

            var result = base.SaveChangesAsync(cancellationToken);

            //如果手工开启了自动事务，用完后关闭。
            if (isManualTransaction)
                Database.AutoTransactionsEnabled = false;

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => _entityInfo.OnModelCreating(modelBuilder);

        protected virtual int SetAuditFields()
        {
            var operater = _entityInfo.GetOperater();
            var allBasicAuditEntities = ChangeTracker.Entries<ICreateAuditInfo>().Where(x => x.State == EntityState.Added);
            allBasicAuditEntities.ForEach(entry =>
            {
                entry.Entity.CreateBy = operater.Id;
                entry.Entity.CreateTime = DateTime.Now;
            });

            var auditFullEntities = ChangeTracker.Entries<IModifyAuditInfo>().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            auditFullEntities.ForEach(entry =>
            {
                entry.Entity.ModifyBy = operater.Id;
                entry.Entity.ModifyTime = DateTime.Now;
            });

            return ChangeTracker.Entries<Entity>().Count();
        }
    }
}
