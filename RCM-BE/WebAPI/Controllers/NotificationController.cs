using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotification _iNotification;
		public NotificationController(INotification iNotification)
		{
			_iNotification = iNotification;
		}

		#region SendNotification
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sendNotificationReqViewModel"></param>
		/// <returns>SendNotificationAsync</returns>
		/// <response code="200">Sent Notification successfully</response>
		/// <response code="400">Sent Notification failed</response>

		[HttpPost("SendNotification")]
		public async Task<CommonResponse> SendNotificationAsync(SendNotificationReqViewModel sendNotificationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = await _iNotification.SendNotificationAsync(sendNotificationReqViewModel.Adapt<SendNotificationReqDTO>());
				SendNotificationResDTO sendNotificationResDTO = commonResponse.Data;
				commonResponse.Data = sendNotificationResDTO.Adapt<SendNotificationResViewModel>();

			}
			catch (Exception) { throw; }
			return commonResponse;
		}
        #endregion

        #region GetAllAdminNotification
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getAllNotificationReqViewModel"></param>
        /// <returns>GetAllAdminNotificationAsync</returns>
		/// <response code="200">Get All Admin Notification successfully</response>
		/// <response code="400">Sent Notification failed</response>
        [HttpPost("GetAllAdminNotification")]		
		public async Task<CommonResponse> GetAllAdminNotificationAsync(GetAllNotificationReqViewModel getAllNotificationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = await _iNotification.GetAllAdminNotificationAsync(getAllNotificationReqViewModel.Adapt<GetAllNotificationReqDTO>());
				GetAllNotificationResDTO getAllAdminNotificationResDTO = commonResponse.Data;
				commonResponse.Data = getAllAdminNotificationResDTO.Adapt<GetAllNotificationResViewModel>();
			}
			catch(Exception) { throw; }
			return commonResponse;
		}
        #endregion

        #region UpdateNotification
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns>UpdateNotificationAsync</returns>
		/// <response code="200">Update Notification successfully</response>
		/// <response code="400">Update Notification failed</response>

        [HttpPost("UpdateNotification")]
		public async Task<CommonResponse> UpdateNotificationAsync(UpdateNotificationReqViewModel updateNotificationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
                commonResponse = await _iNotification.UpdateNotificationAsync(updateNotificationReqViewModel.Adapt<UpdateNotificationReqDTO>());
                UpdateNotificationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<UpdateNotificationResViewModel>();
            }
			catch(Exception) { throw; }
			return commonResponse;
		}
		#endregion

		#region GetAllReceptionistNotification
		[HttpPost("GetAllReceptionistNotification")]
		public async Task<CommonResponse> GetAllReceptionistNotificationAsync(GetAllReceptionistNotificationReqViewModel getAllReceptionistNotificationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = await _iNotification.GetAllReceptionistNotificationAsync(getAllReceptionistNotificationReqViewModel.Adapt<GetAllReceptionistNotificationReqDTO>());
				GetAllReceptionistNotificationResDTO model = commonResponse.Data;
				commonResponse.Data = model.Adapt<GetAllReceptionistNotificationResViewModel>();
			}
			catch (Exception) { throw; }
			return commonResponse;
		}
		#endregion
	}
}
