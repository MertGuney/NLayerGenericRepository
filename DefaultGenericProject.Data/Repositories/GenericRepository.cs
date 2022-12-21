using DefaultGenericProject.Core.Enums;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DefaultGenericProject.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<TEntity> GetAll(DataStatus? dataStatus = DataStatus.Active)
        {
            return _dbSet.Where(x => x.Status == dataStatus).AsNoTracking().AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(DataStatus? dataStatus = DataStatus.Active)
        {
            return await _dbSet.Where(x => x.Status == dataStatus).ToListAsync();
        }

        public TEntity GetById(Guid id, DataStatus? dataStatus = DataStatus.Active)
        {
            var entity = _dbSet.Where(x => x.Id == id && x.Status == dataStatus).FirstOrDefault();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id, DataStatus? dataStatus = DataStatus.Active)
        {
            var entity = await _dbSet.Where(x => x.Id == id && x.Status == dataStatus).FirstOrDefaultAsync();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            return _dbSet.Include(expression);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void SetStatus(TEntity entity, DataStatus dataStatus)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = dataStatus;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void SetInactive(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = DataStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Update(entity);
            return entity;
        }

        public TEntity UpdateEntryState(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}