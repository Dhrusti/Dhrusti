using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplNotification : INotification
	{
		private readonly NotificationBLL _notificationBLL; 
		public ImplNotification(NotificationBLL notificationBLL)
		{
			_notificationBLL = notificationBLL;
		}

		public async Task<CommonResponse> SendNotificationAsync(SendNotificationReqDTO sendNotificationReqDTO)
		{
			return await _notificationBLL.SendNotificationAsync(sendNotificationReqDTO);
		}

		public async Task<CommonResponse> GetAllAdminNotificationAsync(GetAllNotificationReqDTO getAllNotificationReqDTO)
		{
			return await _notificationBLL.GetAllAdminNotificationAsync(getAllNotificationReqDTO);
		}

        public async Task<CommonResponse> UpdateNotificationAsync(UpdateNotificationReqDTO updateNotificationReqDTO)
		{
			return await _notificationBLL.UpdateNotificationAsync(updateNotificationReqDTO);
        }
		public async Task<CommonResponse> GetAllReceptionistNotificationAsync(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO)
		{
			return await _notificationBLL.GetAllReceptionistNotificationAsync(getAllReceptionistNotificationReqDTO);
		}


	}
}
