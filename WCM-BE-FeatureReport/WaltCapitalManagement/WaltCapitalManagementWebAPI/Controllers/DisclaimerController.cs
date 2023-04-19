using DTO.ReqDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisclaimerController : ControllerBase
    {
        private readonly IDisclaimer _idisclaimer;
        public DisclaimerController(IDisclaimer idisclaimer)
        {
            _idisclaimer = idisclaimer;
        }

        [HttpPost("AddDisclaimer")]
        public CommonResponse AddDisclaimer(AddDisclaimerReqViewModel addDisclaimerReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _idisclaimer.AddDisclaimer(addDisclaimerReqViewModel.Adapt<AddDisclaimerReqDTO>());
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
