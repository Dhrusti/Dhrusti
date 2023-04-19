using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface INotification
    {
        Task<CommonResponse> SendNotification(NotificationReqDTO notificationReqDTO);
    }
}
