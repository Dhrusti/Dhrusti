using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAccountType
    {
        public CommonResponse GetAllAccountType();

        public CommonResponse GetAccountTypeById(GetAccountTypeReqDTO getAccountTypeReqDTO);

        public CommonResponse AddAccountType(AddAccountTypeReqDTO addAccountTypeReq);

        public CommonResponse UpdateAccountType(UpdateAccountTypeReqDTO updateAccountTypeReq);

        public CommonResponse DeleteAccountType(DeleteAccountTypeReqDTO deleteAccountTypeReqDTO);
    }
}
