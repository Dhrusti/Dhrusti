using DTO;
using Microsoft.AspNetCore.SignalR;

namespace WaltCapitalManagementWebAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public Task NotifyAll(Notification notification) =>
            Clients.All.SendAsync("NotificationReceived", notification);
    }
}
