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
    public class ClientStatementController : ControllerBase
    {
        private readonly IClientStatement _iclientStatement;
        public ClientStatementController(IClientStatement iclientStatement)
        {
            this._iclientStatement = iclientStatement;
        }

        [HttpPost("GetReportType")]
        public CommonResponse GetReportType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientStatement.GetReportType();
                GetReportTypeResDTO model = commonResponse.Data ?? new GetReportTypeResDTO();
                commonResponse.Data = model.Adapt<GetReportTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetClientStatementByFundId")]
        public CommonResponse GetClientStatementByFundId(GetClientStatementByFundIdReqViewModel getClientStatementReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientStatement.GetClientStatementByFundId(getClientStatementReqViewModel.Adapt<GetClientStatementByFundIdReqDTO>());
                GetClientStatementByFundIdResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetClientStatementByFundIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetClientStatementReport")]
        public CommonResponse GetClientStatementReport(GetClientStatementReportReqViewModel getClientStatementReportReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientStatement.GetClientStatementReport(getClientStatementReportReqViewModel.Adapt<GetClientStatementReportReqDTO>());
                GetClientStatementReportResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetClientStatementReportResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
