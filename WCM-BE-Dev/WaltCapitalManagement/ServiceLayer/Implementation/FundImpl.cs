using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class FundImpl : IFund
    {
        private readonly FundBLL _fundBLL;

        public FundImpl(FundBLL fundBLL)
        {
            _fundBLL = fundBLL;
        }

        public CommonResponse GetAllFundList()
        {
            return _fundBLL.GetAllFundList();
        }

        public CommonResponse GetFundList(GetFundListReqDTO getFundListByStatusReqDTO)
        {
            return _fundBLL.GetFundList(getFundListByStatusReqDTO);
        }
        public CommonResponse AddFund(AddFundReqDTO addFundReqDTO)
        {
            return _fundBLL.AddFund(addFundReqDTO);
        }
        public CommonResponse DeleteFund(DeleteFundReqDTO deleteFundReqDTO)
        {
            return _fundBLL.DeleteFund(deleteFundReqDTO);
        }
        public CommonResponse GetFundById(GetFundByIdReqDTO getFundByIdReqDTO)
        {
            return _fundBLL.GetFundById(getFundByIdReqDTO);
        }
        public CommonResponse UpdateFundStatus(UpdateFundStatusReqDTO updateFundReqDTO)
        {
            return _fundBLL.UpdateFundStatus(updateFundReqDTO);
        }
        public CommonResponse UpdateFund(UpdateFundReqDTO updateFundReqDTO)
        {
            return _fundBLL.UpdateFund(updateFundReqDTO);
        }
    }
}
