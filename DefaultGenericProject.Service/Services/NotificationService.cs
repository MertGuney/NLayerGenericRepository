using DefaultGenericProject.Core.DTOs.Notifications;
using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotification(SendNotificationDTO sendNotificationDTO)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = sendNotificationDTO.Title,
                    Body = sendNotificationDTO.Body,
                },
                Token = sendNotificationDTO.Token,
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            await messaging.SendAsync(message);
        }

        public async Task<Response<ResponseMessageDTO>> SendMultipleNotification(SendMultipleNotificationDTO sendMultipleNotificationDTO)
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(Environment.CurrentDirectory, "wwwroot/key.json"))
                });
            }

            var multiCastMessage = new MulticastMessage()
            {
                Notification = new Notification
                {
                    Title = sendMultipleNotificationDTO.Title,
                    Body = sendMultipleNotificationDTO.Body
                },
                Tokens = sendMultipleNotificationDTO.Token
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            await messaging.SendMulticastAsync(multiCastMessage);
            return Response<ResponseMessageDTO>.Success(new ResponseMessageDTO { Message = "Bildirim gönderilmiştir.", IsSuccessful = true }, 200);
        }
    }
}