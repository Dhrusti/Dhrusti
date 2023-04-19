using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUser
    {
        public CommonResponse AddClientPhase1(AddClientPhase1ReqDTO addClientPhase1ReqDTO);
        public CommonResponse AddClientPhase2(AddClientPhase2ReqDTO addClientPhase2ReqDTO);
        public CommonResponse UpdateClientPhase1(UpdateClientPhase1ReqDTO updateClientPhase1ReqDTO);
        public CommonResponse GetAllClient(GetAllClientReqDTO getClientReqDTO);
        public CommonResponse GetByClientId(GetByClientIdReqDTO getByClientIdReqDTO);
        public CommonResponse UploadDocument(UploadDocumentReqDTO uploadDocumentReqDTO);
        public CommonResponse GenerateAccountNo(GenerateAccountNoReqDTO generateAccountNoReqDTO);
        public CommonResponse GetAllClientList();
        public CommonResponse GetClientAllDetailById(GetClientAllDetailByIdReqDTO getClientAllDetailByIdReqDTO);
    }
}
