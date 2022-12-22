using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}