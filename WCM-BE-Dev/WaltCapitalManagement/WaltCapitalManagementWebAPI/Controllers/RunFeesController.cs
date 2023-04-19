using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunFeesController : ControllerBase
    {
        private readonly IRunFees _irunFees;

        public RunFeesController(IRunFees irunFees)
        {
            _irunFees = irunFees;
        }

        [HttpPost("GetRunFees")]
        public CommonResponse GetRunFees(GetRunFeesReqViewModels getRunFeesReqViewModels)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _irunFees.GetRunFees(getRunFeesReqViewModels.Adapt<GetRunFeesReqDTO>());
                GetRunFeesResDTO getRunFeesResDTO = commonResponse.Data;
                commonResponse.Data = getRunFeesResDTO.Adapt<GetRunFeesResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("CalculateRunFees")]
        public CommonResponse CalculateRunFees(CalculateRunFeesReqViewModel calculateRunFeesReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _irunFees.CalculateRunFees(calculateRunFeesReqViewModel.Adapt<CalculateRunFeesReqDTO>());
                GetRunFeesResDTO getRunFeesResDTO = commonResponse.Data;
                commonResponse.Data = getRunFeesResDTO.Adapt<GetRunFeesResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
