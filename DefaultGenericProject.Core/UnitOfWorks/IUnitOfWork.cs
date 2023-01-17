using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Repositories;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }

        Task CommmitAsync();

        void Commit();
    }
}