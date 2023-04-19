using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class IFAImpl : IIFA
    {
        private readonly IFABLL _iFABLL;
        public IFAImpl(IFABLL iFABLL)
        {
            _iFABLL = iFABLL;
        }

        public CommonResponse GetAllIFA()
        {
            return _iFABLL.GetAllIFA();
        }

        public CommonResponse GetIFAById(GetIFAReqDTO getIFAReqDTO)
        {
            return _iFABLL.GetIFAById(getIFAReqDTO);
        }

        public CommonResponse AddIFAPhase1(AddIFAReqDTO addIFAReqDTO)
        {
            return _iFABLL.AddIFAPhase1(addIFAReqDTO);
        }

        public CommonResponse AddIFAPhase2(AddIFAPhase2ReqDTO addIFAPhase2ReqDTO)
        {
            return _iFABLL.AddIFAPhase2(addIFAPhase2ReqDTO);
        }

        public CommonResponse UpdateIFA(UpdateIFAReqDTO updateIFAReqDTO)
        {
            return _iFABLL.UpdateIFA(updateIFAReqDTO);
        }

        public CommonResponse DeleteIFA(DeleteIFAReqDTO deleteIFAReqDTO)
        {
            return _iFABLL.DeleteIFA(deleteIFAReqDTO);
        }

        public CommonResponse GetAllIFAClient(GetIFAClientReqDTO getIFAClientReqDTO)
        {
            return _iFABLL.GetAllIFAClient(getIFAClientReqDTO);
        }

        public CommonResponse GetAllIFAList(GetIFAAssetReqDTO getIFAAssetReqDTO)
        {
            return _iFABLL.GetAllIFAList(getIFAAssetReqDTO);
        }

        public CommonResponse GenerateIFAAccountNo()
        {
            return _iFABLL.GenerateIFAAccountNo();
        }

    }
}
