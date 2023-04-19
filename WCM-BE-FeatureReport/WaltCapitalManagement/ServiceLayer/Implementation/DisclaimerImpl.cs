using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class DisclaimerImpl : IDisclaimer
    {
        private readonly DisclaimerBLL _disclaimerBLL;

        public DisclaimerImpl(DisclaimerBLL disclaimerBLL)
        {
            _disclaimerBLL = disclaimerBLL;
        }
        public CommonResponse AddDisclaimer(AddDisclaimerReqDTO addDisclaimerReqDTO)
        {
            return _disclaimerBLL.AddDisclaimer(addDisclaimerReqDTO);
        }
    }
}
