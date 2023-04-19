using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notification;

        public NotificationController(INotification notification)
        { 
            _notification = notification;
        }
        [HttpPost("SendNotification")]
        public CommonResponse SendNotification(SendNotificationReqViewModel sendNotificationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _notification.SendNotification(sendNotificationReqViewModel.Adapt<SendNotificationReqDTO>());
                SendNotificationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<SendNotificationResViewModel>();


            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        [HttpPost("GetAllAdminNotification")]
        public CommonResponse GetAllNotification(GetAllNotificationReqViewModel getAllNotificationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _notification.GetAllNotification(getAllNotificationReqViewModel.Adapt<GetAllNotificationReqDTO>());
                GetAllNotificationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllNotificationResViewModel>();

            }
            catch (Exception ex)
            {

            }
            return commonResponse;
        }

        [HttpPost("UpdateNotification")]
        public CommonResponse UpdateNotification(UpdateNotificationReqViewModel updateNotificationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _notification.UpdateNotification(updateNotificationReqViewModel.Adapt<UpdateNotificationReqDTO>());
                UpdateNotificationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<UpdateNotificationResViewModel>();

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        [HttpPost("GetAllReceptionistNotification")]
        public CommonResponse GetAllReceptionistNotification(GetAllReceptionistNotificationReqViewModel getAllReceptionistNotificationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _notification.GetAllReceptionistNotification(getAllReceptionistNotificationReqViewModel.Adapt<GetAllReceptionistNotificationReqDTO>());
                GetAllReceptionistNotificationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllReceptionistNotificationResViewModel>();

            }
            catch (Exception ex)
            {

            }
            return commonResponse;
        }
    }
}
