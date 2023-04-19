using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IServiceProviderDetails
    {
        public CommonResponse GetAllServiceProvider();
        public CommonResponse GetByServiceProviderId(GetServiceProviderByIdReqDTO getServiceProviderByIdReqDTO);
        public CommonResponse AddServiceProvider(AddServiceProviderReqDTO addServiceProviderReqDTO);
        public CommonResponse UpdateServiceProvider(UpdateServiceProviderReqDTO updateServiceProviderReqDTO);
        public CommonResponse DeleteServiceProvider(DeleteServiceProviderReqDTO deleteServiceProviderReqDTO);
    }
}
