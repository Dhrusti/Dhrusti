using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace MVC_Project.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAdminDashboard _iAdminDashboard;
        
        public DashboardController(IAdminDashboard iAdminDashboard)
        {
            _iAdminDashboard = iAdminDashboard;
        }

        public IActionResult Index()
        {
            return View();
        }

		public async Task<ActionResult> Dashboard(GetAllAdminAppointmentReqViewModel getAllAdminAppointmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
			commonResponse = await _iAdminDashboard.GetAllAdminAppointmentAsync(getAllAdminAppointmentReqViewModel.Adapt<GetAllAdminAppointmentReqDTO>());
            GetAllAdminAppointmentResDTO getAllAdminAppointmentResDTO = commonResponse.Data;
            //var A = getAllAdminAppointmentResDTO.Adapt<GetAllAdminAppointmentResViewModel>();
            commonResponse.Data = getAllAdminAppointmentResDTO.Adapt<GetAllAdminAppointmentResViewModel>();
			if (commonResponse.Status)
			{
				ViewBag.Data = commonResponse.Data;
				return View("Dashboard", "Dashboard");
			}
            //var userData = commonResponse.Data;
			//ViewBag.Data = userData;
			
			ViewBag.message = commonResponse.Message;
			return View("Dashboard");
        }

    }
}
