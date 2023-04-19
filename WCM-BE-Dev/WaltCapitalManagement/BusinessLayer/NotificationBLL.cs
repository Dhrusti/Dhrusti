using DTO.ReqDTO;
using Helper;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLayer
{
    public class NotificationBLL
    {
        private readonly NotificationSettingsReqDTO _fcmNotificationSetting;
        private readonly CommonHelper _commonHelper;
        public NotificationBLL(IOptions<NotificationSettingsReqDTO> settings, CommonHelper commonHelper)
        {
            _fcmNotificationSetting = settings.Value;
            _commonHelper = commonHelper;
        }
        public async Task<CommonResponse> SendNotification(NotificationReqDTO notificationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (await _commonHelper.SendNotificationAsync(notificationReqDTO))
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Notification Sent Successfully!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can't Send Notification!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
