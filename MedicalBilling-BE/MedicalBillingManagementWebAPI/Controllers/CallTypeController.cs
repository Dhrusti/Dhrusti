using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallTypeController : ControllerBase
    {
        private readonly ICallType _callType;

        public CallTypeController(ICallType callType)
        {
            _callType = callType;
        }

        [HttpGet("GetAllCallType")]
        public CommonResponse GetDoctorList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _callType.GetCallTypeList();
                List<GetAllCallTypeResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllCallTypeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
    }
}
