using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IClientType
    {
        public CommonResponse GetAllClientType();
        public CommonResponse GetByClientTypeId(GetClientTypeByIdReqDTO getClientTypeByIdReqDTO);
        public CommonResponse AddClientType(AddClientTypeReqDTO addClientTypeReqDTO);
        public CommonResponse UpdateClientType(UpdateClientTypeReqDTO updateClientTypeReqDTO);
        public CommonResponse DeleteClientType(DeleteClientTypeReqDTO deleteClientTypeReqDTO);
    }
}
