using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IDisclaimer
    {
        public CommonResponse AddDisclaimer(AddDisclaimerReqDTO addDisclaimerReqDTO);
    }
}
