using Microsoft.AspNetCore.SignalR;

namespace BlazorWebAssemblySignalRApp.Server.Hubs
{
    public static class SignalRMethodName
    {
        public const string ReceiveAddGroupMessage = "ReceiveAddGroupMessage";
        public const string ReceiveGroupMessage = "ReceiveGroupMessage";
    }

    //References link: https://ithelp.ithome.com.tw/articles/10203383

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync(SignalRMethodName.ReceiveAddGroupMessage, $"{userName} add to group {groupName}");
        }

        public async Task SendMessageToGroup(string groupName, string userName, string message)
        {
            await Clients.Group(groupName).SendAsync(SignalRMethodName.ReceiveGroupMessage, groupName, userName, message);
        }
    }
}
