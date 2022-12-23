using DefaultGenericProject.Core.DTOs.Notifications;
using DefaultGenericProject.Core.DTOs.Responses;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.Services
{
    public interface INotificationService
    {
        Task SendNotification(SendNotificationDTO sendNotificationDTO);
        Task<Response<ResponseMessageDTO>> SendMultipleNotification(SendMultipleNotificationDTO sendMultipleNotificationDTO);
    }
}