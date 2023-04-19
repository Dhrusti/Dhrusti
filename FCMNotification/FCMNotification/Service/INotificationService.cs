using FCMNotification.Models;

namespace FCMNotification.Service
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
