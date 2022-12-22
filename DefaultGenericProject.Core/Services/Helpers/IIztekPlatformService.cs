using DefaultGenericProject.Core.DTOs.Helpers;
using DefaultGenericProject.Core.DTOs.Responses;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services.Helpers
{
    public interface IIztekPlatformService
    {
        Task<Response<ResponseMessageDTO>> SendMailAsync(SendMailDTO sendMail);
    }
}