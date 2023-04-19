using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IClientTransaction
    {
        public CommonResponse GetAllClientTransactionByFundId(GetAllClientTransactionByFundIdReqDTO getAllClientTansactionByFundIdReqDTO);
        public CommonResponse AddClientTransaction(AddClientTransactionReqDTO addClientTransactionDTO);

        public CommonResponse UpdateClientTransaction(UpdateClientTransactionReqDTO updateClientTransactionDTO);
        public CommonResponse GetFundForCTByFundId(GetFundForCTByFundIdReqDTO getFundForCTByFundIdReqDTO);
        public CommonResponse GetAllIFAbyClientId(GetAllIFAByClientIdReqDTO getAllIFAByClientIdReqDTO);
        public CommonResponse GetByClientTransactionId(GetByClientTransactionIdReqDTO getByClientTransactionIdReqDTO);
        public CommonResponse DeleteClientTransaction(DeleteClientTransactionReqDTO deleteClientTransactionReqDTO);
        public CommonResponse GetTranscationTypeByClientId(GetTranscationTypeByClientIdReqDTO getTranscationTypeByClientIdReqDTO);
        public CommonResponse GetTranscationUnitTypeByClientId(GetTransactionUnitTypeByClientIdReqDTO getTransactionUnitTypeByClientIdReqDTO);
    }
}
