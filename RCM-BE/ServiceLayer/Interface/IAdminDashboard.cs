using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
	public interface IAdminDashboard
	{
		public Task<CommonResponse> GetAllAdminAppointmentAsync(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO);
	}
}
