using System.Collections.Generic;

namespace DefaultGenericProject.Core.DTOs.Notifications
{
    public class SendMultipleNotificationDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> Token { get; set; }
    }
}