using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class UserImpl : IUser
    {
        private readonly UserBLL _userBLL;

        public UserImpl(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        public CommonResponse AddClientPhase1(AddClientPhase1ReqDTO addClientPhase1ReqDTO)
        {
            return _userBLL.AddClientPhase1(addClientPhase1ReqDTO);
        }
        public CommonResponse AddClientPhase2(AddClientPhase2ReqDTO addClientPhase2ReqDTO)
        {
            return _userBLL.AddClientPhase2(addClientPhase2ReqDTO);
        }
        public CommonResponse GetAllClient(GetAllClientReqDTO getClientReqDTO)
        {
            return _userBLL.GetAllClient(getClientReqDTO);
        }
        public CommonResponse GetByClientId(GetByClientIdReqDTO getByClientIdReqDTO)
        {
            return _userBLL.GetByClientId(getByClientIdReqDTO);
        }
        public CommonResponse UpdateClientPhase1(UpdateClientPhase1ReqDTO updateClientPhase1ReqDTO)
        {
            return _userBLL.UpdateClientPhase1(updateClientPhase1ReqDTO);
        }
        public CommonResponse UploadDocument(UploadDocumentReqDTO uploadDocumentReqDTO)
        {
            return _userBLL.UploadDocument(uploadDocumentReqDTO);
        }
        public CommonResponse GenerateAccountNo(GenerateAccountNoReqDTO generateAccountNoReqDTO)
        {
            return _userBLL.GenerateAccountNo(generateAccountNoReqDTO);
        }
        public CommonResponse GetAllClientList()
        {
            return _userBLL.GetAllClientList();
        }
    }
}
