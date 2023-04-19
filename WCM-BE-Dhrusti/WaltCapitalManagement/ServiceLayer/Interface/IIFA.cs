using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IIFA
    {
        public CommonResponse GetAllIFA();
        public CommonResponse GetIFAById(GetIFAReqDTO getIFAReqDTO);
        public CommonResponse AddIFAPhase1(AddIFAReqDTO addIFAReqDTO);
        public CommonResponse AddIFAPhase2(AddIFAPhase2ReqDTO addIFAPhase2ReqDTO);
        public CommonResponse UpdateIFA(UpdateIFAReqDTO updateIFAReqDTO);
        public CommonResponse DeleteIFA(DeleteIFAReqDTO deleteIFAReqDTO);
        public CommonResponse GetAllIFAClient(GetIFAClientReqDTO getIFAClientReqDTO);
        public CommonResponse GetAllIFAList(GetIFAAssetReqDTO getIFAAssetReqDTO);
        public CommonResponse GenerateIFAAccountNo();
       

    }
}
