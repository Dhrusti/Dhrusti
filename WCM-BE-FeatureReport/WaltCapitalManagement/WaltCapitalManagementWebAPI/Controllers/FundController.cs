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
    public class FundController : ControllerBase
    {
        private readonly IFund _ifund;

        public FundController(IFund ifund)
        {
            _ifund = ifund;
        }

        [HttpPost("GetAllFundList")]
        public CommonResponse GetAllFundList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.GetAllFundList();
                List<GetFundResDTO> getFundResDTOs = commonResponse.Data ?? new List<GetFundResDTO>();
                commonResponse.Data = getFundResDTOs.Adapt<List<GetFundResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetFundList")]
        public CommonResponse GetFundList(GetFundListReqViewModel getFundListByStatusReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.GetFundList(getFundListByStatusReqViewModel.Adapt<GetFundListReqDTO>());
                List<GetFundListResDTO> getFundListByStatusResDTO = commonResponse.Data ?? new List<GetFundListResDTO>();
                commonResponse.Data = getFundListByStatusResDTO.Adapt<List<GetFundListResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddFund")]
        public CommonResponse AddFund(AddFundReqViewModel addFundReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.AddFund(addFundReqViewModel.Adapt<AddFundReqDTO>());
                AddFundResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddFundResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteFund")]
        public CommonResponse DeleteFund(DeleteFundReqViewModel deleteFundReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.DeleteFund(deleteFundReqViewModel.Adapt<DeleteFundReqDTO>());
                DeleteFundResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteFundResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetFundById")]
        public CommonResponse GetFundById(GetFundByIdReqViewModel getFundByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.GetFundById(getFundByIdReqViewModel.Adapt<GetFundByIdReqDTO>());
                //AddFundResDTO Model = commonResponse.Data;
                //commonResponse.Data = Model.Adapt<AddFundResViewModel>();

            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateFundStatus")]
        public CommonResponse UpdateFundStatus(UpdateFundStatusReqViewModel updateFundReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.UpdateFundStatus(updateFundReqViewModel.Adapt<UpdateFundStatusReqDTO>());
                UpdateFundStatusResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateFundStatusResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateFund")]
        public CommonResponse UpdateFund(UpdateFundReqViewModel addFundReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifund.UpdateFund(addFundReqViewModel.Adapt<UpdateFundReqDTO>());
                UpdateFundResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateFundResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
