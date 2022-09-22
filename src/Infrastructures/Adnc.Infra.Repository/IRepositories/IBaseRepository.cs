using Adnc.Infra.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.IRepositories
{
    public interface IBaseRepository<TEntity> : IRepository<TEntity>
               where TEntity : Entity, IEntity<long>
    {
        #region Ef仓储的基类接口
        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities"><see cref="TEntity"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询实体是否存在
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="writeDb">是否读写库，默认false,可选参数</param>
        /// param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计符合条件的实体数量
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="writeDb">是否读写库，默认false,可选参数</param>
        /// param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询，返回IQueryable<TEntity>
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <param name="writeDb">是否读写库，默认false,可选参数</param>
        /// <param name="noTracking">是否开启跟踪，默认false,可选参数</param>
        /// <returns></returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, bool writeDb = false, bool noTracking = true);
        #endregion

        #region DDD
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(long keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default);
        #endregion

        #region 三层模式开发
        /// <summary>
        /// 返回IQueryable<TEntity>
        /// </summary>
        /// <param name="writeDb">是否读写库，默认false,可选参数</param>
        /// <param name="noTracking">是否开启跟踪，默认false,可选参数</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(bool writeDb = false, bool noTracking = true);

        /// <summary>
        /// 根据Id查询,返回单个实体
        /// </summary>
        /// <param name="keyValue">Id</param>
        /// <param name="navigationPropertyPath">导航属性,可选参数</param>
        /// <param name="writeDb">是否读写库,默认false，可选参数</param>
        /// <param name="noTracking">是否开启跟踪，默认不开启，可选参数</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> FindAsync(long keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询,返回单个实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="navigationPropertyPath">导航属性,可选参数</param>
        /// <param name="orderByExpression">排序字段，默认主键，可选参数</param>
        /// <param name="ascending">排序方式，默认逆序，可选参数</param>
        /// <param name="writeDb">是否读写库,默认false，可选参数</param>
        /// <param name="noTracking">是否开启跟踪，默认不开启，可选参数</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = false, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询,返回单个实体或对象
        /// </summary>
        /// <typeparam name="TResult">匿名对象</typeparam>
        /// <param name="selector">选择器</param>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段，默认主键，可选参数</param>
        /// <param name="ascending">排序方式，默认逆序，可选参数</param>
        /// <param name="writeDb">是否读写库,默认false，可选参数</param>
        /// <param name="noTracking">是否开启跟踪，默认不开启，可选参数</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<TResult> FetchAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = false, bool writeDb = false, bool noTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"><see cref="entity"/></param>
        /// <param name="updatingExpressions">需要更新列的表达式树数组</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>>[] updatingExpressions, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="updatingExpression">需要更新的字段</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updatingExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="propertyNameAndValues">需要更新的字段与值</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(Dictionary<long, List<(string propertyName, dynamic propertyValue)>> propertyNameAndValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue">Id</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteAsync(long keyValue, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<int> DeleteRangeAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default); 
        #endregion

    }
}
