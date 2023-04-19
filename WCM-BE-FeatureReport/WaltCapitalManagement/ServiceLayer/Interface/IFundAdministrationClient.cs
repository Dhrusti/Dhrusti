using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IFundAdministrationClient
    {
        public CommonResponse GetAllFundAdministrationClient(GetFundAdministrationClientReqDTO getFundAdministrationClientReqDTO);
    }
}
