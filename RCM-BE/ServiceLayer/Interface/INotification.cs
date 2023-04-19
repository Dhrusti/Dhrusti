using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
	public interface INotification
	{
		public Task<CommonResponse> SendNotificationAsync(SendNotificationReqDTO sendNotificationReqDTO);

		public Task<CommonResponse> GetAllAdminNotificationAsync(GetAllNotificationReqDTO getAllNotificationReqDTO);

		public Task<CommonResponse> UpdateNotificationAsync(UpdateNotificationReqDTO updateNotificationReqDTO);

		public Task<CommonResponse> GetAllReceptionistNotificationAsync(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO);


	}
}
