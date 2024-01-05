using Framework.Entity;
using Framework.Service.Extension;
using Framework.Service.Utilities.Criteria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Repository
{
    public abstract class GenericRepository<TDbContext, TEntity> : RepositoryBase<TDbContext, TEntity>, IGenericRepository<TDbContext, TEntity>
        where TDbContext : BaseDbContext<TDbContext>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        protected readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericQueryRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="TDbContext"/>.</param>
        protected GenericRepository(TDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// The FetchAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async Task<IEnumerable<TEntity>> FetchAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async Task<TEntity> FetchByIdAsync(long id)
        {
            var entity = await DbSet.FindAsync(id).ConfigureAwait(false);
            _dbContext.Entry(entity).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <param name="selector">The selector<see cref="Expression{Func{TEntity, TResult}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TResult}}"/>.</returns>
        public async Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return await DbSet.AsNoTracking().Where(predicate).Select(selector).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByAndReturnQuerable.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        public IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        /// <summary>
        /// The FetchByCriteriaAsync.
        /// </summary>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IList{TEntity}}"/>.</returns>
        public async Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria)
        {
            var query = DbSet.AsNoTracking()
                .AddPredicate(criteria)
                .AddIncludes(criteria)
                //.AddPaging(criteria)
                .AddSorting(criteria);
            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async virtual Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The UpdateOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        public virtual IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return entities;
        }

        /// <summary>
        /// The InsertOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The SaveOnly.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SaveOnly()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async virtual Task<bool> Delete(long id)
        {
            var entity = await DbSet.FindAsync(id).ConfigureAwait(false);
            _dbContext.Set<TEntity>().Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        public async Task DeleteOnly(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }
    }
}
