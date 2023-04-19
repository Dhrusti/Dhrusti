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
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolio _iportfolio;
        public PortfolioController(IPortfolio iportfolio)
        {
            _iportfolio = iportfolio;
        }

        [HttpPost("GetPortFolioData")]
        public CommonResponse GetPortFolioData(PortFolioDataReqViewModel portFolioDataReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iportfolio.GetPortfolioData(portFolioDataReqViewModel.Adapt<PortFolioDataReqDTO>());
                PortFolioDataResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<PortFolioDataResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetPortfolioCSVData")]
        public CommonResponse GetPortfolioCSVData(GetPortfolioCSVDataReqViewModel getPortfolioCSVDataReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iportfolio.GetPortfolioCSVData(getPortfolioCSVDataReqViewModel.Adapt<GetPortfolioCSVDataReqDTO>());
                GetPortfolioCSVDataResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetPortfolioCSVDataResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetPortfolioClientData")]
        public CommonResponse GetPortfolioClientData(GetPortfolioClientDataReqViewModel getPortfolioClientDataReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iportfolio.GetPortfolioClientData(getPortfolioClientDataReqViewModel.Adapt<GetPortfolioClientDataReqDTO>());

                GetPortfolioClientDataResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetPortfolioClientDataResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("GetPortfolioClientList")]
        public CommonResponse GetPortfolioClientList(GetPortfolioClientListReqViewModel getPortfolioClientListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iportfolio.GetPortfolioClientList(getPortfolioClientListReqViewModel.Adapt<GetPortfolioClientListReqDTO>());

                GetPortfolioClientListResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetPortfolioClientListResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
