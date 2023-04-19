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
    public class FeesController : ControllerBase
    {
        private readonly IFees _iFees;

        public FeesController(IFees iFees)
        {
            _iFees = iFees;
        }

        [HttpPost("GetIFAFeeBreakdown")]
        public CommonResponse GetIFAFeeBreakdown(GetIFAFeeBreakdownReqDTO getIFAFeeBreakdownReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFees.GetIFAFeeBreakdown(getIFAFeeBreakdownReqDTO);
                GetIFAFeeBreakdownResDTO getFundResDTOs = commonResponse.Data ?? new GetIFAFeeBreakdownResDTO();
                commonResponse.Data = getFundResDTOs.Adapt<GetIFAFeeBreakdownResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetClientDetailsByIfaId")]
        public CommonResponse GetClientDetailsByIfaId(GetClientDetailsByIfaIdReqViewModel getClientDetailsByIfaIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFees.GetClientDetailsByIfaId(getClientDetailsByIfaIdReqViewModel.Adapt<GetClientDetailsByIfaIdReqDTO>());
                List<GetClientDetailsByIfaIdResDTO> getClientDetailsByIfa = commonResponse.Data ?? new List<GetClientDetailsByIfaIdResDTO>();
                commonResponse.Data = getClientDetailsByIfa.Adapt<List<GetClientDetailsByIfaIdResViewModel>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("GetIfaFeeReport")]
        public CommonResponse GetIfaFeeReport(GetIfaFeeReportReqViewModel getIfaFeeReportReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFees.GetIfaFeeReport(getIfaFeeReportReqViewModel.Adapt<GetIfaFeeReportReqDTO>());
                GetIfaFeeReportResDTO getIfaFeeReportRes = commonResponse.Data ?? new GetIfaFeeReportResDTO();
                commonResponse.Data = getIfaFeeReportRes.Adapt<GetIfaFeeReportResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
