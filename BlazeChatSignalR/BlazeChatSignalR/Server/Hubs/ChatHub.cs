using Microsoft.AspNetCore.SignalR;

namespace BlazeChatSignalR.Server.Hubs
{
    public class ChatHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await AddMessageToChat("","User Connected..!!");
            await base.OnConnectedAsync();
        }
        public async Task AddMessageToChat(string user,string message)
        {
            await Clients.All.SendAsync("GetThatMessage",user, message);
        }
    }
}
