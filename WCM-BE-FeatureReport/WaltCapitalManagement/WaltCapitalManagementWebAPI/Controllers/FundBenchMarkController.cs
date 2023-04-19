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
    public class FundBenchMarkController : ControllerBase
    {
        private readonly IFundBenchMark _ifundBenchMark;

        public FundBenchMarkController(IFundBenchMark ifundBenchMark)
        { 
            _ifundBenchMark = ifundBenchMark;
        }

        [HttpPost("GetAllFundBenchMark")]
        public CommonResponse GetAllFundBenchMark(GetFundBenchMarkReqViewModel getFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.GetAllFundBenchMark(getFundBenchMarkReqViewModel.Adapt<GetFundBenchMarkReqDTO>());
                GetFundBenchMarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetFundBenchMarkResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        } 
        [HttpPost("GetAllUpdateFundBenchMark")]
        public CommonResponse GetAllUpdateFundBenchMark(GetAllUpdateFundBenchMarkReqViewModel getAllUpdateFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.GetAllUpdateFundBenchMark(getAllUpdateFundBenchMarkReqViewModel.Adapt<GetAllUpdateFundBenchMarkReqDTO>());
                List<GetAllUpdateFundBenchMarkResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetAllUpdateFundBenchMarkResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddFundBenchMark")]
        public CommonResponse AddFundBenchMark(AddFundBenchMarkReqViewModel addFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.AddFundBenchMark(addFundBenchMarkReqViewModel.Adapt<AddFundBenchMarkReqDTO>());
                AddFundBenchMarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddFundBenchMarkResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("UpdateFundBenchMark")]
        public CommonResponse UpdateFundBenchMark(UpdateFundBenchMarkReqViewModel updateFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.UpdateFundBenchMark(updateFundBenchMarkReqViewModel.Adapt<UpdateFundBenchMarkReqDTO>());
                UpdateFundBenchMarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateFundBenchMarkResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateAddStatusFundBenchMark")]
        public CommonResponse UpdateAddStatusFundBenchMark(UpdateStatusFundBenchMarkReqViewModel updateStatusFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.UpdateAddStatusFundBenchMark(updateStatusFundBenchMarkReqViewModel.Adapt<UpdateStatusFundBenchMarkReqDTO>());
                UpdateStatusFundBenchMarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateStatusFundBenchMarkResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("UpdateRemoveStatusFundBenchMark")]
        public CommonResponse UpdateRemoveStatusFundBenchMark(UpdateRemoveStatusFundBenchMarkReqViewModel updateRemoveStatusFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.UpdateRemoveStatusFundBenchMark(updateRemoveStatusFundBenchMarkReqViewModel.Adapt<UpdateRemoveStatusFundBenchMarkReqDTO>());
                UpdateRemoveStatusFundBenchMarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateRemoveStatusFundBenchMarkResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        //GetAllDashboarFundBenchMark

        [HttpPost("GetAllDashboarFundBenchMark")]
        public CommonResponse GetAllDashboarFundBenchMark(GetAllDashboardFundBenchMarkReqViewModel getAllDashboardFundBenchMarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundBenchMark.GetAllDashboarFundBenchMark(getAllDashboardFundBenchMarkReqViewModel.Adapt<GetAllDashboardFundBenchMarkReqDTO>());
                List<GetAllFundBenchMarkDashboardResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetAllFundBenchMarkDashboardResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
