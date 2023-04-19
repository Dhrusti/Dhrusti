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
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboard _adminDashboard;

        public AdminDashboardController(IAdminDashboard adminDashboard)
        {
            _adminDashboard = adminDashboard;
        }

        [HttpPost("GetAllAdminAppointment")]
        public CommonResponse GetAllAdminAppointment(GetAllAdminAppointmentReqViewModel getAllAdminAppointmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _adminDashboard.GetAllAdminAppointment(getAllAdminAppointmentReqViewModel.Adapt<GetAllAdminAppointmentReqDTO>());
                GetAllAdminAppointmentResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllAdminAppointmentResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
        [HttpPost("UpdateApprovalStatus")]
        public CommonResponse UpdateApprovalStatus(UpdateApprovalStatusReqViewModel updateApprovalStatusReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _adminDashboard.UpdateApprovalStatus(updateApprovalStatusReqViewModel.Adapt<UpdateApprovalStatusReqDTO>());
                UpdateApprovalStatusResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<UpdateApprovalStatusResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }

    }
}
