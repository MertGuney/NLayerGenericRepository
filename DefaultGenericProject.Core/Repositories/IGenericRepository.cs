using DefaultGenericProject.Core.Enums;
using DefaultGenericProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(Guid id, DataStatus? dataStatus = DataStatus.Active);
        Task<TEntity> GetByIdAsync(Guid id, DataStatus? dataStatus = DataStatus.Active);
        IQueryable<TEntity> GetAll(DataStatus? dataStatus = DataStatus.Active);
        Task<IEnumerable<TEntity>> GetAllAsync(DataStatus? dataStatus = DataStatus.Active);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Include(Expression<Func<TEntity, object>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity UpdateEntryState(TEntity entity);
        void SetStatus(TEntity entity, DataStatus dataStatus);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}