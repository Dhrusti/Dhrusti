using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IOffshore
    {
        public CommonResponse GetOffshoreClientList(GetOffshoreClientReqDTO getOffshoreClientReqDTO);
    }
}
