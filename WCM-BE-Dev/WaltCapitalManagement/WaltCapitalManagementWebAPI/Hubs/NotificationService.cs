using DTO;
using Microsoft.AspNetCore.SignalR;

namespace WaltCapitalManagementWebAPI.Hubs
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext) =>
            _hubContext = hubContext;

        public Task SendNotificationAsync(Notification notification) =>
            notification is not null
                ? _hubContext.Clients.All.SendAsync("NotificationReceived", notification)
                : Task.CompletedTask;
    }
}
