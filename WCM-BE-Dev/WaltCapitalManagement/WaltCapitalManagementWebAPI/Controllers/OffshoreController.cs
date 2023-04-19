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
    public class OffshoreController : ControllerBase
    {
        private readonly IOffshore _iOffshore;
        public OffshoreController(IOffshore iOffshore)
        {
            _iOffshore = iOffshore;
        }

        [HttpPost("GetOffshoreClientList")]
        public CommonResponse GetOffshoreClientList(GetOffshoreClientReqViewModel getOffshoreClientReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffshore.GetOffshoreClientList(getOffshoreClientReqViewModel.Adapt<GetOffshoreClientReqDTO>());
                if (commonResponse.Data != null)
                {
                    GetOffshoreClientResDTO getOffshoreClientResDTO = commonResponse.Data ?? new GetOffshoreClientResDTO();
                    commonResponse.Data = getOffshoreClientResDTO.Adapt<GetOffshoreClientResViewModel>();
                }
                //GetOffshoreClientResDTO getOffshoreClientResDTO = commonResponse.Data ?? new GetOffshoreClientResDTO();
                //commonResponse.Data = getOffshoreClientResDTO.Adapt<GetOffshoreClientResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
