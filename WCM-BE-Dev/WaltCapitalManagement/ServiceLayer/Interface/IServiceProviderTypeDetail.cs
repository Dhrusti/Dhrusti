using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IServiceProviderTypeDetail
    {
        public CommonResponse GetAllServiceProviderType();
        public CommonResponse GetByServiceProviderTypeId(GetServiceProviderTypeByIdReqDTO getServiceProviderTypeByIdReqDTO);
        public CommonResponse AddServiceProviderType(AddServiceProviderTypeReqDTO addServiceProviderTypeReqDTO);
        public CommonResponse UpdateServiceProviderType(UpdateServiceProviderTypeReqDTO updateServiceProviderTypeReqDTO);
        public CommonResponse DeleteServiceProviderType(DeleteServiceProviderTypeReqDTO deleteServiceProviderTypeReqDTO);
    }
}
