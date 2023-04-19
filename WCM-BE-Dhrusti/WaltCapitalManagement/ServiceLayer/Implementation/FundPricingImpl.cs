using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class FundPricingImpl : IFundPricing
    {
        private readonly FundPricingBLL _fundPricingBLL;

        public FundPricingImpl(FundPricingBLL fundPricingBLL)
        {
            _fundPricingBLL = fundPricingBLL;
        }

        public CommonResponse GetPricing(GetPricingReqDTO getPricingReqDTO)
        {
            return _fundPricingBLL.GetPricing(getPricingReqDTO);
        }

        public CommonResponse GetAddPricingDetail(GetAddPricingDetailReqDTO getAddPricingDetailReqDTO)
        {
            return _fundPricingBLL.GetAddPricingDetail(getAddPricingDetailReqDTO);
        }

        public CommonResponse AddPricing(AddPricingReqDTO addPricingReqDTO)
        {
            return _fundPricingBLL.AddPricing(addPricingReqDTO);
        }

        public CommonResponse GetEditPricingDetail(GetEditPricingDetailReqDTO getEditPricingDetailReqDTO)
        {
            return _fundPricingBLL.GetEditPricingDetail(getEditPricingDetailReqDTO);
        }

        public CommonResponse UpdatePricing(UpdatePricingReqDTO updatePricingReqDTO)
        {
            return _fundPricingBLL.UpdatePricing(updatePricingReqDTO);
        }
    }
}
