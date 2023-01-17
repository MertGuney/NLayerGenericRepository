using DefaultGenericProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Repositories
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Read
        TEntity GetById(Guid id, bool isTracking = true);
        Task<TEntity> GetByIdAsync(Guid id, bool isTracking = true);
        IQueryable<TEntity> GetAll(bool isTracking = true);
        Task<IEnumerable<TEntity>> GetAllAsync(bool isTracking = true);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool isTracking = true);
        IQueryable<TEntity> Include(Expression<Func<TEntity, object>> expression, bool isTracking = true);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region Write
        Task SaveChangesAsync();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity UpdateEntryState(TEntity entity);
        void SetInactive(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion
    }
}