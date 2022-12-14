using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        #region Example
        //public async Task<List<ChatHistoryDTO>> AddToGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    return _groupRepository.Where(x => x.GroupName == groupName).Select(x => new ChatHistoryDTO
        //    {
        //        Username = x.Username,
        //        Content = x.Content,
        //        Date = x.CreatedDate
        //    }).OrderBy(x => x.Date).ToList();
        //}
        #endregion

        public async Task GroupSendMessage(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("receiveMessage", user, message);
        }
    }
}
