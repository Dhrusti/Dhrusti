using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AccountTypeImpl : IAccountType
    {
        private readonly AccountTypeBLL _accountTypeBLL;

        public AccountTypeImpl(AccountTypeBLL accountTypeBLL)
        {
            _accountTypeBLL = accountTypeBLL;
        }

        public CommonResponse GetAllAccountType()
        {
            return _accountTypeBLL.GetAllAccountType();
        }

        public CommonResponse GetAccountTypeById(GetAccountTypeReqDTO getAccountTypeReqDTO)
        {
            return _accountTypeBLL.GetAccountTypeById(getAccountTypeReqDTO);
        }

        public CommonResponse AddAccountType(AddAccountTypeReqDTO addAccountTypeReq)
        {
            return _accountTypeBLL.AddAccountType(addAccountTypeReq);
        }

        public CommonResponse UpdateAccountType(UpdateAccountTypeReqDTO updateAccountTypeReq)
        {
            return _accountTypeBLL.UpdateAccountType(updateAccountTypeReq);
        }

        public CommonResponse DeleteAccountType(DeleteAccountTypeReqDTO deleteAccountTypeReqDTO)
        {
            return _accountTypeBLL.DeleteAccountType(deleteAccountTypeReqDTO);
        }
    }
}
