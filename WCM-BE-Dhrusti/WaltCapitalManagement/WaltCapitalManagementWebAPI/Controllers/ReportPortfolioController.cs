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
    public class ReportPortfolioController : ControllerBase
    {
        private readonly IReportPortfolio _reportPortfolio;
        public ReportPortfolioController(IReportPortfolio reportPortfolio)
        {
            _reportPortfolio = reportPortfolio;
        }

        [HttpPost("GetPortfolioManagerFee")]
        public CommonResponse GetPortfolioManagerFee(GetPortfolioManagerFeeReqViewModel getPortfolioManagerFeeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _reportPortfolio.GetPortfolioManagerFee(getPortfolioManagerFeeReqViewModel.Adapt<GetPortfolioManagerFeeReqDTO>());

                GetPortfolioManagerFeeResDTO getPortfolioManagerFeeRes = commonResponse.Data ?? new GetPortfolioManagerFeeResDTO();
                commonResponse.Data = getPortfolioManagerFeeRes.Adapt<GetPortfolioManagerFeeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("GetPPMClientList")]
        public CommonResponse GetPPMClientList(GetPPMClientListReqViewModel getPPMClientListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = _reportPortfolio.GetPPMClientList(getPPMClientListReqViewModel.Adapt<GetPPMClientListReqDTO>());

                GetPPMClientListResDTO getPPMClientListRes = commonResponse.Data ?? new GetPPMClientListResDTO();
                commonResponse.Data = getPPMClientListRes.Adapt<GetPPMClientListResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        } 
        [HttpPost("GePPMClientListTFSA")]
        public CommonResponse GetPPMClientListTFSA(GetPPMClientListTFSAReqViewModel getPPMClientListTFSAReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = _reportPortfolio.GetPPMClientListTFSA(getPPMClientListTFSAReqViewModel.Adapt<GetPPMClientListTFSAReqDTO>());

                GetPPMClientListTFSAResDTO getPPMClientListTFSARes = commonResponse.Data ?? new GetPPMClientListTFSAResDTO();
                commonResponse.Data = getPPMClientListTFSARes.Adapt<GetPPMClientListTFSAResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
