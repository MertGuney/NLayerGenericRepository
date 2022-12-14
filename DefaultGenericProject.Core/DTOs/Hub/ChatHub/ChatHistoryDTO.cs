using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGenericProject.Core.DTOs.Hub.ChatHub
{
    public class ChatHistoryDTO
    {
        public string User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
