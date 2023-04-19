using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ServiceProviderImpl : IServiceProviderDetails
    {
        private readonly ServiceProviderBLL _serviceProviderBLL;

        public ServiceProviderImpl(ServiceProviderBLL serviceProviderBLL)
        {
            _serviceProviderBLL = serviceProviderBLL;
        }

        public CommonResponse GetAllServiceProvider()
        {
            return _serviceProviderBLL.GetAllServiceProvider();
        }

        public CommonResponse GetByServiceProviderId(GetServiceProviderByIdReqDTO getServiceProviderByIdReqDTO)
        {

            return _serviceProviderBLL.GetByServiceProviderId(getServiceProviderByIdReqDTO);

        }
        public CommonResponse AddServiceProvider(AddServiceProviderReqDTO addServiceProviderReqDTO)
        {

            return _serviceProviderBLL.AddServiceProvider(addServiceProviderReqDTO);

        }
        public CommonResponse UpdateServiceProvider(UpdateServiceProviderReqDTO updateServiceProviderReqDTO)
        {

            return _serviceProviderBLL.UpdateServiceProvider(updateServiceProviderReqDTO);

        }
        public CommonResponse DeleteServiceProvider(DeleteServiceProviderReqDTO deleteServiceProviderReqDTO)
        {

            return _serviceProviderBLL.DeleteServiceProvider(deleteServiceProviderReqDTO);

        }
    }
}
