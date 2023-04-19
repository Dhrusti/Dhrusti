using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class FundAdministrationClientImpl : IFundAdministrationClient
    {
        private readonly FundAdministrationClientBLL _fundAdministrationClientBLL;
        public FundAdministrationClientImpl(FundAdministrationClientBLL fundAdministrationClientBLL)
        {
            _fundAdministrationClientBLL = fundAdministrationClientBLL;
        }
        public CommonResponse GetAllFundAdministrationClient(GetFundAdministrationClientReqDTO getFundAdministrationClientReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _fundAdministrationClientBLL.GetAllFundAdministrationClient(getFundAdministrationClientReqDTO);
            return commonResponse;
        }
    }
}
