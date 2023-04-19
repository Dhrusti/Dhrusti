using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class NotificationImpl : INotification
    {
        private readonly NotificationBLL _notificationBLL;
        public NotificationImpl(NotificationBLL notificationBLL)
        {
            _notificationBLL = notificationBLL;
        }

        public Task<CommonResponse> SendNotification(NotificationReqDTO notificationReqDTO)
        {
            return _notificationBLL.SendNotification(notificationReqDTO);
        }
    }
}
