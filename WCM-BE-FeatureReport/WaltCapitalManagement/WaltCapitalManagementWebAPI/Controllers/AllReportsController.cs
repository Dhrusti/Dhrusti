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
    public class AllReportsController : ControllerBase
    {
        private readonly IAllReports _iallReports;

        public AllReportsController(IAllReports iallReports)
        {
            _iallReports = iallReports;
        }

        [HttpPost("GetTradeStationClientList")]
        public CommonResponse GetTradeStationClientList(GetTradeStationClientListReqViewModel getTradeStationClientListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iallReports.GetTradeStationClientList(getTradeStationClientListReqViewModel.Adapt<GetTradeStationClientListReqDTO>());
                GetTradeStationClientListResDTO getTradeStationClientList = commonResponse.Data ?? new GetTradeStationClientListResDTO();
                commonResponse.Data = getTradeStationClientList.Adapt<GetTradeStationClientListResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("InteractiveBrokersClientList")]
        public CommonResponse InteractiveBrokersClientList(GetInteractiveBrokersClientListReqViewModel getInteractiveBrokersClientListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iallReports.InteractiveBrokersClientList(getInteractiveBrokersClientListReqViewModel.Adapt<GetInteractiveBrokersClientListReqDTO>());
                GetInteractiveBrokersClientListResDTO getInteractiveBrokersClientListRes = commonResponse.Data ?? new GetInteractiveBrokersClientListResDTO();
                commonResponse.Data = getInteractiveBrokersClientListRes.Adapt<GetInteractiveBrokersClientListResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("AllenGrayClientList")]
        public CommonResponse AllenGrayClientList(GetAllenGrayClientListReqViewModel getAllenGrayClientListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iallReports.AllenGrayClientList(getAllenGrayClientListReqViewModel.Adapt<GetAllenGrayClientListReqDTO>());
                GetAllenGrayClientListResDTO getAllenGrayClientListRes = commonResponse.Data ?? new GetAllenGrayClientListResDTO();
                commonResponse.Data = getAllenGrayClientListRes.Adapt<GetAllenGrayClientListResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
