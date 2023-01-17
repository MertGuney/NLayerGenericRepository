using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        #region Read
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression) => await Table.AnyAsync(expression);

        public IQueryable<TEntity> GetAll(bool isTracking = true) => isTracking ? Table : Table.AsNoTracking();

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool isTracking = true) => isTracking ? await Table.ToListAsync() : await Table.AsNoTracking().ToListAsync();

        public TEntity GetById(Guid id, bool isTracking = true) => isTracking ? Table.Find(id) : Table.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public async Task<TEntity> GetByIdAsync(Guid id, bool isTracking = true) => isTracking ? await Table.FindAsync(id) : await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> expression, bool isTracking = true) => isTracking ? Table.Include(expression).AsQueryable() : Table.AsNoTracking().Include(expression).AsQueryable();

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool isTracking = true) => isTracking ? Table.Where(predicate).AsQueryable() : Table.AsNoTracking().Where(predicate).AsQueryable();
        #endregion

        #region Write
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task AddAsync(TEntity entity) => await Table.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await Table.AddRangeAsync(entities);

        public void Remove(TEntity entity) => Table.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => Table.RemoveRange(entities);

        public void SetInactive(TEntity entity)
        {
            entity.RemovedDate = DateTime.Now;
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
        #endregion
    }
}
