using DTO;
using Helper;
using Microsoft.AspNetCore.SignalR.Client;

namespace WaltCapitalManagementWebAPI.Hubs
{
    public sealed class Consumer : IAsyncDisposable
    {
        private readonly string HostDomain = Environment.GetEnvironmentVariable("HOST_DOMAIN");

        private HubConnection _hubConnection;
        private CommonHelper _commonHelper;

        public Consumer()
        {
            _hubConnection = new HubConnectionBuilder()
                //.WithUrl(new Uri($"{HostDomain}/hub/notifications"))
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<Notification>(
            "NotificationReceived", OnNotificationReceivedAsync);
        }

        public Task StartNotificationConnectionAsync() =>
            _hubConnection.StartAsync();

        public Task SendNotificationAsync(string text) =>
    _hubConnection.InvokeAsync(
        "NotifyAll", new Notification(text, _commonHelper.GetCurrentDateTime()));

        private async Task OnNotificationReceivedAsync(Notification notification)
        {
            _commonHelper.AddLog($"{HostDomain} zxcvxzcvzvasdaksdjaksjdlkjalkdsj");
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }
}
