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
	public class AdminDashboardController : ControllerBase
	{
		private readonly IAdminDashboard _iAdminDashboard;
		public AdminDashboardController(IAdminDashboard iAdminDashboard)
		{
			_iAdminDashboard = iAdminDashboard;
		}

		#region GetAllAdminAppointmentAsync
		/// <summary>
		/// Get all admin apointment list
		/// </summary>
		/// <param name=""></param>
		/// <returns>GetAllAdminAppointment</returns>
		/// <response code="200">GetAllAdminAppointment success</response>
		/// <response code="400">GetAllAdminAppointment failed</response>
		/// 
		[HttpPost("GetAllAdminAppointmentAsync")]
		public async Task<CommonResponse> GetAllAdminAppointmentAsync(GetAllAdminAppointmentReqViewModel getAllAdminAppointmentReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = await _iAdminDashboard.GetAllAdminAppointmentAsync(getAllAdminAppointmentReqViewModel.Adapt<GetAllAdminAppointmentReqDTO>());
				GetAllAdminAppointmentResDTO getAllAdminAppointmentResDTO = commonResponse.Data;
				commonResponse.Data = getAllAdminAppointmentResDTO.Adapt<GetAllAdminAppointmentResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion
	}
}
