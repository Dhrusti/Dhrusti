using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundAdministrationClientController : ControllerBase
    {
        private readonly IFundAdministrationClient _ifundAdministration;

        public FundAdministrationClientController(IFundAdministrationClient ifundAdministration)
        {
            _ifundAdministration = ifundAdministration;
        }

        [HttpPost("GetAllFundAdministrationClient")]
        public CommonResponse GetAllFundAdministrationClient(GetFundAdministrationClientReqViewModel getFundAdministrationClientReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundAdministration.GetAllFundAdministrationClient(getFundAdministrationClientReqViewModel.Adapt<GetFundAdministrationClientReqDTO>());
                GetFundAdministrationClientResDTO getFundAdministrationClientResDTO = commonResponse.Data ?? new GetFundAdministrationClientResDTO();
                commonResponse.Data = getFundAdministrationClientResDTO.Adapt<GetFundAdmClientResViewModel>();

            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
