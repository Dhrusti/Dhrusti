using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IExternalAccount
    {
        public CommonResponse AddExternalAccount(AddExternalAccountReqDTO addExternalAccountReqDTO);
        public CommonResponse GetAllExternalAccountByUserId(GetAllExternalAccountReqDTO getAllExternalAccountReqDTO);
        public CommonResponse UpdateupdateExternalAccount(UpdateExternalAccountReqDTO updateExternalAccountReqDTO);
        public CommonResponse DeleteExternalAccount(DeleteExternalAccountReqDTO deleteExternalAccountReqDTO);
    }
}
