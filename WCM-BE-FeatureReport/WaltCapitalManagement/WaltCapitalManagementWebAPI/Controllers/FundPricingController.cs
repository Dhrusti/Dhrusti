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
    public class FundPricingController : ControllerBase
    {
        private readonly IFundPricing _ifundPricing;
        public FundPricingController(IFundPricing ifundPricing)
        {
            _ifundPricing = ifundPricing;
        }

        [HttpPost("GetPricing")]
        public CommonResponse GetPricing(GetPricingReqViewModel getPricingReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundPricing.GetPricing(getPricingReqViewModel.Adapt<GetPricingReqDTO>());
                GetPricingResDTO getPricingResDTOs = commonResponse.Data ?? new GetPricingResDTO();
                commonResponse.Data = getPricingResDTOs.Adapt<GetPricingResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAddPricingDetail")]
        public CommonResponse GetAddPricingDetail(GetAddPricingDetailReqViewModel getAddPricingDetailReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundPricing.GetAddPricingDetail(getAddPricingDetailReqViewModel.Adapt<GetAddPricingDetailReqDTO>());
                GetAddPricingDetailResDTO getAddPricingDetailResDTOs = commonResponse.Data ?? new GetAddPricingDetailResDTO();
                commonResponse.Data = getAddPricingDetailResDTOs.Adapt<GetAddPricingDetailResViewModel>();

                //new GetAddPricingDetailResViewModel();
                //if (commonResponse.Data != null)
                //{
                //    commonResponse.Data = getAddPricingDetailResViewModel;
                //}

            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddPricing")]
        public CommonResponse AddPricing(AddPricingReqViewModel addPricingReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _ifundPricing.AddPricing(addPricingReqViewModel.Adapt<AddPricingReqDTO>());

                //GetAddPricingDetailResViewModel getAddPricingDetailResViewModel = new GetAddPricingDetailResViewModel();
                //if (commonResponse.Data != null)
                //{
                //    getAddPricingDetailResViewModel.DynamicPricingInputFields = commonResponse.Data;
                //    commonResponse.Data = getAddPricingDetailResViewModel;
                //}

            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
