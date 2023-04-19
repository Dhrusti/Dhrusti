using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class OffshoreImpl : IOffshore
    {
        private readonly OffshoreBLL _iOffshoreBLL;
        public OffshoreImpl(OffshoreBLL iOffshoreBLL)
        {
            _iOffshoreBLL = iOffshoreBLL;
        }

        public CommonResponse GetOffshoreClientList(GetOffshoreClientReqDTO getOffshoreClientReqDTO)
        {
            return _iOffshoreBLL.GetOffshoreClientList(getOffshoreClientReqDTO);
        }
    }
}
