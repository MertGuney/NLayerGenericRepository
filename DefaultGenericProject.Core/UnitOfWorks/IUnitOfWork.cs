using System.Threading.Tasks;

namespace DefaultGenericProject.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommmitAsync();

        void Commit();
    }
}