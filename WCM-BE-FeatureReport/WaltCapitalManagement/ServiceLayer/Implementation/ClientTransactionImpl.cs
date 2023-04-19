using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ClientTransactionImpl : IClientTransaction
    {
        private readonly ClientTransactionBLL _ClientTransactionBLL;

        public ClientTransactionImpl(ClientTransactionBLL clientTransactionBLL)
        {
            _ClientTransactionBLL = clientTransactionBLL;
        }

        public CommonResponse GetAllClientTransactionByFundId(GetAllClientTransactionByFundIdReqDTO getAllClientTansactionByFundIdReqDTO)
        {
            return _ClientTransactionBLL.GetAllClientTransactionByFundId(getAllClientTansactionByFundIdReqDTO);
        }
        public CommonResponse AddClientTransaction(AddClientTransactionReqDTO addClientTransactionReqDTO)
        {
            return _ClientTransactionBLL.AddClientTransaction(addClientTransactionReqDTO);
        }
        public CommonResponse UpdateClientTransaction(UpdateClientTransactionReqDTO updateClientTransactionReqDTO)
        {
            return _ClientTransactionBLL.UpdateClientTransaction(updateClientTransactionReqDTO);
        }
        public CommonResponse GetFundForCTByFundId(GetFundForCTByFundIdReqDTO getFundForCTByFundIdReqDTO)
        {
            return _ClientTransactionBLL.GetFundForCTByFundId(getFundForCTByFundIdReqDTO);
        }
        public CommonResponse GetAllIFAbyClientId(GetAllIFAByClientIdReqDTO getAllIFAByClientIdReqDTO)
        {
            return _ClientTransactionBLL.GetAllIFAbyClientId(getAllIFAByClientIdReqDTO);
        }
        public CommonResponse GetByClientTransactionId(GetByClientTransactionIdReqDTO getByClientTransactionIdReqDTO)
        {
            return _ClientTransactionBLL.GetByClientTransactionId(getByClientTransactionIdReqDTO);
        }
        public CommonResponse DeleteClientTransaction(DeleteClientTransactionReqDTO deleteClientTransactionReqDTO)
        {
            return _ClientTransactionBLL.DeleteClientTransaction(deleteClientTransactionReqDTO);
        }
        public CommonResponse GetTranscationTypeByClientId(GetTranscationTypeByClientIdReqDTO getTranscationTypeByClientIdReqDTO)
        {
            return _ClientTransactionBLL.GetTranscationTypeByClientId(getTranscationTypeByClientIdReqDTO);
        } 
        public CommonResponse GetTranscationUnitTypeByClientId(GetTransactionUnitTypeByClientIdReqDTO getTransactionUnitTypeByClientIdReqDTO)
        {
            return _ClientTransactionBLL.GetTranscationUnitTypeByClientId(getTransactionUnitTypeByClientIdReqDTO);
        }

    }
}
