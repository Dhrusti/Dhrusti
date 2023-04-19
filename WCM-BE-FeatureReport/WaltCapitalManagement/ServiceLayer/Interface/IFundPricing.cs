using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IFundPricing
    {
        public CommonResponse GetPricing(GetPricingReqDTO getPricingReqDTO);

        public CommonResponse GetAddPricingDetail(GetAddPricingDetailReqDTO getAddPricingDetailReqDTO);
        public CommonResponse AddPricing(AddPricingReqDTO addPricingReqDTO);
    }
}
