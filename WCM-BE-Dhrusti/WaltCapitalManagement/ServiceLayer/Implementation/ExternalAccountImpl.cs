using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ExternalAccountImpl : IExternalAccount
    {
        private readonly ExternalAccountBLL _iexternalAccountBLL;

        public ExternalAccountImpl(ExternalAccountBLL externalAccountBLL)
        {
            this._iexternalAccountBLL = externalAccountBLL;
        }
        public CommonResponse AddExternalAccount(AddExternalAccountReqDTO addExternalAccountReqDTO)
        {
            return _iexternalAccountBLL.AddExternalAccount(addExternalAccountReqDTO);
        }
        public CommonResponse GetAllExternalAccountByUserId(GetAllExternalAccountReqDTO getAllExternalAccountReqDTO)
        {
            return _iexternalAccountBLL.GetAllExternalAccountByUserId(getAllExternalAccountReqDTO);
        }
        public CommonResponse DeleteExternalAccount(DeleteExternalAccountReqDTO deleteExternalAccountReqDTO)
        {
            return _iexternalAccountBLL.DeleteExternalAccount(deleteExternalAccountReqDTO);
        }
        public CommonResponse UpdateupdateExternalAccount(UpdateExternalAccountReqDTO updateExternalAccountReqDTO)
        {
            return _iexternalAccountBLL.UpdateupdateExternalAccount(updateExternalAccountReqDTO);
        }

    }
}
