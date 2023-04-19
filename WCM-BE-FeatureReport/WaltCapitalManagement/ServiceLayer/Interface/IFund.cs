using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IFund
    {
        public CommonResponse GetAllFundList();
        public CommonResponse GetFundList(GetFundListReqDTO getFundListByStatusReqDTO);
        public CommonResponse AddFund(AddFundReqDTO addFundReqDTO);
        public CommonResponse DeleteFund(DeleteFundReqDTO deleteFundReqDTO);
        public CommonResponse GetFundById(GetFundByIdReqDTO getFundByIdReqDTO);
        public CommonResponse UpdateFundStatus(UpdateFundStatusReqDTO updateFundReqDTO);
        public CommonResponse UpdateFund(UpdateFundReqDTO updateFundReqDTO);

    }
}
