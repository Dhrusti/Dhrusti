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
    public class DurationController : ControllerBase
    {
        private readonly IDuration _duration;

        public DurationController(IDuration duration)
        {
            _duration = duration;
        }
        [HttpPost("AddDuration")]
        public CommonResponse AddDuration(AddDurationReqViewModel addDurationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _duration.AddDuration(addDurationReqViewModel.Adapt<AddDurationReqDTO>());
                AddDurationResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<AddDurationResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;

        }
    }
}   




