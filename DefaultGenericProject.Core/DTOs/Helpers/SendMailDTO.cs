using System.Collections.Generic;

namespace DefaultGenericProject.Core.DTOs.Helpers
{
    public class SendMailDTO
    {
        public string Client { get; set; }
        public List<string> To { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}