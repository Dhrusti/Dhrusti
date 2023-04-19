using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ClientImpl : IClientType
    {
        private readonly ClientTypeBLL _clientTypeBLL;

        public ClientImpl(ClientTypeBLL clientTypeBLL)
        {
            _clientTypeBLL = clientTypeBLL;
        }

        public CommonResponse GetAllClientType()
        {
            return _clientTypeBLL.GetAllClientType();
        }
        public CommonResponse GetByClientTypeId(GetClientTypeByIdReqDTO getClientTypeByIdReqDTO)
        {
            return _clientTypeBLL.GetByClientTypeId(getClientTypeByIdReqDTO);
        }
        public CommonResponse AddClientType(AddClientTypeReqDTO addClientTypeReqDTO)
        {
            return _clientTypeBLL.AddClientType(addClientTypeReqDTO);
        }
        public CommonResponse UpdateClientType(UpdateClientTypeReqDTO updateClientTypeReqDTO)
        {
            return _clientTypeBLL.UpdateClientType(updateClientTypeReqDTO);
        }
        public CommonResponse DeleteClientType(DeleteClientTypeReqDTO deleteClientTypeReqDTO)
        {
            return _clientTypeBLL.DeleteClientType(deleteClientTypeReqDTO);
        }

    }
}
