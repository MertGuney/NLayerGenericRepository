using DefaultGenericProject.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DefaultGenericProject.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }
    }
}
