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
	public class ImplAdminDashboard : IAdminDashboard
	{
		private readonly AdminDashboardBLL _adminDashboardBLL;
		public ImplAdminDashboard(AdminDashboardBLL adminDashboardBLL)
		{
			_adminDashboardBLL = adminDashboardBLL;
		}

		public async Task<CommonResponse> GetAllAdminAppointmentAsync(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO)
		{
			return await _adminDashboardBLL.GetAllAdminAppointmentAsync(getAllAdminAppointmentReqDTO);
		}
	}
}
