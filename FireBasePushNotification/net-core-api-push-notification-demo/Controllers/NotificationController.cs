using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_core_api_push_notification_demo.Models;
using net_core_api_push_notification_demo.Services;

namespace net_core_api_push_notification_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
