using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUserDocument
    {
        public CommonResponse AddUserDocument(UserDocumentReqDTO userDocumentReqDTO);
        public CommonResponse RemoveUserDocument(DeleteAccountTypeReqDTO deleteAccountTypeReqDTO);
    }
}
