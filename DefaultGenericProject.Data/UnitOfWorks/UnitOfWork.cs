using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace DefaultGenericProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IGenericRepository<Product> ProductRepository => new GenericRepository<Product>(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}