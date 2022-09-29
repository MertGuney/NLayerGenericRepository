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
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<Tentity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Tentity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<Tentity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<Tentity> GetAll()
        {
            return _dbSet.Where(x => x.Status == DataStatus.Active).AsNoTracking().AsQueryable();
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.Status == DataStatus.Active).ToListAsync();
        }

        public Tentity GetById(Guid id)
        {
            var entity = _dbSet.Where(x => x.Id == id && x.Status == DataStatus.Active).FirstOrDefault();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<Tentity> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.Where(x => x.Id == id && x.Status == DataStatus.Active).FirstOrDefaultAsync();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public IQueryable<Tentity> Include(Expression<Func<Tentity, object>> expression)
        {
            return _dbSet.Include(expression);
        }

        public void Remove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Tentity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void SetInactive(Tentity entity)
        {
            entity.UpdatedDate= DateTime.Now;
            entity.Status= DataStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Tentity Update(Tentity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Update(entity);
            return entity;
        }

        public Tentity UpdateEntryState(Tentity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}