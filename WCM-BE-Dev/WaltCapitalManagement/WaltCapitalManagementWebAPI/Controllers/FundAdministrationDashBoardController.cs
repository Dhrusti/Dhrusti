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
    public class FundAdministrationDashBoardController : ControllerBase
    {
        private readonly IFundAdministrationDashBoard _ifundAdministrationDashBoard;
        public FundAdministrationDashBoardController(IFundAdministrationDashBoard ifundAdministrationDashBoard)
        {
            _ifundAdministrationDashBoard= ifundAdministrationDashBoard;
        }

        [HttpPost("GetFundAdministrationDashBoardByFundId")]
        public CommonResponse GetFundAdministrationDashBoardByFundId(GetFundAdministrationDashBoardByFundIdReqViewModel getFundAdministrationDashBoardByFundIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundAdministrationDashBoard.GetFundAdministrationDashBoardByFundId(getFundAdministrationDashBoardByFundIdReqViewModel.Adapt<GetFundAdministrationDashBoardByFundIdReqDTO>());
                GetFundAdminDashboardDataResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetFundAdminDashboardDataResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
