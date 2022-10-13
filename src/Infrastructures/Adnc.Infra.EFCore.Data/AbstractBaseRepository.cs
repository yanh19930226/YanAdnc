using Adnc.Infra.Core.System.Extensions.Collection;
using Adnc.Infra.Repository.Entities;
using Adnc.Infra.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Adnc.Infra.EFCore.Data
{
    public abstract class AbstractBaseRepository<TDbContext, TEntity> : IBaseRepository<TEntity>
       where TDbContext : DbContext
       where TEntity : Entity, IEntity<long>
    {
        protected virtual TDbContext DbContext { get; }

        protected AbstractBaseRepository(TDbContext dbContext) => DbContext = dbContext;

        protected virtual IQueryable<TEntity> GetDbSet(bool writeDb, bool noTracking)
        {
            ////不跟踪并且写入数据库
            //if (noTracking && writeDb)
            //    return DbContext.Set<TEntity>().AsNoTracking().TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            ////不跟踪
            //else if (noTracking)
            //    return DbContext.Set<TEntity>().AsNoTracking();
            ////只写入
            //else if (writeDb)
            //    return DbContext.Set<TEntity>().TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            //else
                return DbContext.Set<TEntity>();
        }

        #region 基本

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, bool writeDb = false, bool noTracking = true)=> this.GetDbSet(writeDb, noTracking).Where(expression);

        public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            //if (writeDb)
            //    dbSet = dbSet.TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await EntityFrameworkQueryableExtensions.AnyAsync(dbSet, whereExpression, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            //if (writeDb)
            //    dbSet = dbSet.TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await EntityFrameworkQueryableExtensions.CountAsync(dbSet, whereExpression, cancellationToken);
        }

        public virtual Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            //获取实体状态
            var entry = DbContext.Entry(entity);

            //如果实体没有被跟踪，必须指定需要更新的列
            if (entry.State == EntityState.Detached)
                throw new ArgumentException($"实体没有被跟踪，需要指定更新的列");

            #region removed code

#pragma warning disable S125 // Sections of code should not be commented out
            //实体没有被更改
            //if (entry.State == EntityState.Unchanged)
            //{
            //    var navigations = entry.Navigations.Where(x => x.CurrentValue is ValueObject);
            //    if (navigations?.Count() > 0)
            //    {
            //        foreach (var navigation in navigations)
            //        {
            //            DbContext.Add(navigation.CurrentValue);
            //        }
            //    }
            //    else
            //        return await Task.FromResult(0);
            //}
#pragma warning restore S125 // Sections of code should not be commented out

            #endregion removed code

            //实体被标记为Added或者Deleted，抛出异常，ADNC应该不会出现这种状态。
            if (entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                throw new ArgumentException($"{nameof(entity)},实体状态为{nameof(entry.State)}");

            return this.UpdateInternalAsync(entity, cancellationToken);
        }

        public virtual async Task<int> UpdateInternalAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await DbContext.SaveChangesAsync(cancellationToken);

        #endregion

        #region DDD
        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.UpdateRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.DbContext.Remove(entity);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.RemoveRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(long keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var query = this.GetDbSet(writeDb, false).Where(t => t.Id == keyValue);
            if (navigationPropertyPath == null)
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, cancellationToken);
            else
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.Include(query, navigationPropertyPath), cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(long keyValue, IEnumerable<Expression<Func<TEntity, dynamic>>> navigationPropertyPaths = null, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            if (navigationPropertyPaths == null || navigationPropertyPaths.Count() <= 1)
                return await this.GetAsync(keyValue, navigationPropertyPaths.FirstOrDefault(), writeDb, cancellationToken);

            var query = this.GetDbSet(writeDb, false).Where(t => t.Id == keyValue);
            foreach (var navigationPath in navigationPropertyPaths)
            {
                query = EntityFrameworkQueryableExtensions.Include(query, navigationPath);
            }
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, cancellationToken);
        }
        #endregion

        public virtual async Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default)
    => await DbContext.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);

        public async Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default)
            => await DbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        public virtual IDbTransaction CurrentDbTransaction => DbContext.Database.CurrentTransaction.GetDbTransaction();

        public virtual IQueryable<TEntity> GetAll(bool writeDb = false, bool noTracking = true)
        {
            var queryAble = DbContext.Set<TEntity>().AsQueryable();
            //if (writeDb)
            //    queryAble = queryAble.TagWith(RepositoryConsts.MAXSCALE_ROUTE_TO_MASTER);
            if (noTracking)
                queryAble = queryAble.AsNoTracking();
            return queryAble;
        }

        public virtual async Task<TEntity> FindAsync(long keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default)
        {
            var query = this.GetDbSet(writeDb, noTracking).Where(t => t.Id == keyValue);
            if (navigationPropertyPath != null)
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.Include(query, navigationPropertyPath), cancellationToken);
            else
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, cancellationToken);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = false, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default)
        {
            TEntity result;

            var query = this.GetDbSet(writeDb, noTracking).Where(whereExpression);

            if (navigationPropertyPath != null)
                query = EntityFrameworkQueryableExtensions.Include(query, navigationPropertyPath);

            if (orderByExpression == null)
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderByDescending(x => x.Id), cancellationToken);
            else
                result = ascending
                          ? await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderBy(orderByExpression), cancellationToken)
                          : await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderByDescending(orderByExpression), cancellationToken);
            return result;
        }

        public virtual async Task<TResult> FetchAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = false, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default)
        {
            TResult result;

            var query = this.GetDbSet(writeDb, noTracking).Where(whereExpression);

            if (orderByExpression == null)
                result = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderByDescending(x => x.Id).Select(selector), cancellationToken);
            else
                result = ascending
                          ? await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderBy(orderByExpression).Select(selector), cancellationToken)
                          : await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query.OrderByDescending(orderByExpression).Select(selector), cancellationToken);
            return result;
        }

        public virtual async Task<int> DeleteAsync(long keyValue, CancellationToken cancellationToken = default)
        {
            int rows = 0;
            //查询当前上下文中，有没有同Id实体
            var entity = DbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id == keyValue);

            if (entity != null)
            {
                DbContext.Remove(entity);

                try
                {
                    rows = await DbContext.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    rows = 0;
                }
            }
           
            return rows;
        }

        public virtual async Task<int> DeleteRangeAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            var enityType = typeof(TEntity);
            var hasSoftDeleteMember = typeof(ISoftDelete).IsAssignableFrom(enityType);
            //是否是软删除
            if (hasSoftDeleteMember)
            {
                var newExpression = Expression.New(enityType);
                var paramExpression = Expression.Parameter(enityType, "e");
                var binding = Expression.Bind(enityType.GetMember("IsDeleted")[0], Expression.Constant(true));
                var memberInitExpression = Expression.MemberInit(newExpression, new List<MemberBinding>() { binding });
                var lambdaExpression = Expression.Lambda<Func<TEntity, TEntity>>(memberInitExpression, paramExpression);
                return await DbContext.Set<TEntity>().Where(whereExpression).UpdateAsync(lambdaExpression, cancellationToken);
            }
            //直接删除
            return await DbContext.Set<TEntity>().Where(whereExpression).DeleteAsync(cancellationToken);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>>[] updatingExpressions, CancellationToken cancellationToken = default)
        {
            if (updatingExpressions.IsNullOrEmpty())
                await UpdateAsync(entity, cancellationToken);

            var entry = DbContext.Entry(entity);

            #region 实体状态不对
            if (entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                throw new ArgumentException($"{nameof(entity)},实体状态为{nameof(entry.State)}");
            #endregion

            if (entry.State == EntityState.Unchanged)
                return await Task.FromResult(0);

            if (entry.State == EntityState.Modified)
            {
                var propNames = updatingExpressions.Select(x => x.GetMemberName()).ToArray();
                entry.Properties.ForEach(propEntry =>
                {
                    if (!propNames.Contains(propEntry.Metadata.Name))
                        propEntry.IsModified = false;
                });
            }

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Unchanged;
                updatingExpressions.ForEach(expression =>
                {
                    entry.Property(expression).IsModified = true;
                });
            }

            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> UpdateRangeAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updatingExpression, CancellationToken cancellationToken = default)
        {
            #region 该实体有RowVersion列，不能使用批量更新

            var enityType = typeof(TEntity);
            var hasConcurrencyMember = typeof(IConcurrency).IsAssignableFrom(enityType);

            if (hasConcurrencyMember)
                throw new ArgumentException("该实体有RowVersion列，不能使用批量更新");

            #endregion

            return UpdateRangeInternalAsync(whereExpression, updatingExpression, cancellationToken);
        }

        public virtual async Task<int> UpdateRangeAsync(Dictionary<long, List<(string propertyName, dynamic propertyValue)>> propertyNameAndValues, CancellationToken cancellationToken = default)
        {
            var existsEntities = DbContext.Set<TEntity>().Local.Where(x => propertyNameAndValues.ContainsKey(x.Id));

            foreach (var item in propertyNameAndValues)
            {
                var enity = existsEntities?.FirstOrDefault(x => x.Id == item.Key);

                if (enity != null) {

                    var entry = DbContext.Entry(enity);
                    if (entry.State == EntityState.Detached)
                        entry.State = EntityState.Unchanged;

                    if (entry.State == EntityState.Unchanged)
                    {
                        var info = propertyNameAndValues.FirstOrDefault(x => x.Key == item.Key).Value;
                        info.ForEach(x =>
                        {
                            entry.Property(x.propertyName).CurrentValue = x.propertyValue;
                            entry.Property(x.propertyName).IsModified = true;
                        });
                    }
                }
            }

            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        private  async Task<int> UpdateRangeInternalAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updatingExpression, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().Where(whereExpression).UpdateAsync(updatingExpression, cancellationToken);
        }

    }
}
