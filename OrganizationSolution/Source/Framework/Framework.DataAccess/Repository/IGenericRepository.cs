using Framework.Entity;
using Framework.Service.Utilities.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Repository
{
    /// <summary>
    /// Defines the <see cref="IGenericRepository{TDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IGenericRepository<TDbContext, TEntity> : IRepositoryBase
        where TDbContext : BaseDbContext<TDbContext>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The FetchAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> FetchAllAsync();

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// The FetchByCriteriaAsync.
        /// </summary>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IList{TEntity}}"/>.</returns>
        Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria);

        /// <summary>
        /// The FetchByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> FetchByIdAsync(long id);

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <param name="selector">The selector<see cref="Expression{Func{TEntity, TResult}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TResult}}"/>.</returns>
        Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// The FetchByAndReturnQuerable.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> Insert(TEntity entity);

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(long id);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(IEnumerable<TEntity> entity);

        Task DeleteOnly(IEnumerable<TEntity> entity);

        /// <summary>
        /// The UpdateOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities);

        /// <summary>
        /// The InsertOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities);

        /// <summary>
        /// The SaveOnly.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task SaveOnly();
    }
}
