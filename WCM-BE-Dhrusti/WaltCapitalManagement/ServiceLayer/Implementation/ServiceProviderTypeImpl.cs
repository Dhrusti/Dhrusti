using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ServiceProviderTypeImpl : IServiceProviderTypeDetail
    {
        private readonly ServiceProviderTypeBLL _serviceProviderTypeBLL;

        public ServiceProviderTypeImpl(ServiceProviderTypeBLL serviceProviderTypeBLL)
        {
            _serviceProviderTypeBLL = serviceProviderTypeBLL;
        }

        public CommonResponse GetAllServiceProviderType()
        {
            return _serviceProviderTypeBLL.GetAllServiceProviderType();
        }

        public CommonResponse GetByServiceProviderTypeId(GetServiceProviderTypeByIdReqDTO getServiceProviderTypeByIdReqDTO)
        {

            return _serviceProviderTypeBLL.GetByServiceProviderTypeId(getServiceProviderTypeByIdReqDTO);

        }
        public CommonResponse AddServiceProviderType(AddServiceProviderTypeReqDTO addServiceProviderTypeReqDTO)
        {

            return _serviceProviderTypeBLL.AddServiceProviderType(addServiceProviderTypeReqDTO);

        }
        public CommonResponse UpdateServiceProviderType(UpdateServiceProviderTypeReqDTO updateServiceProviderTypeReqDTO)
        {

            return _serviceProviderTypeBLL.UpdateServiceProviderType(updateServiceProviderTypeReqDTO);

        }
        public CommonResponse DeleteServiceProviderType(DeleteServiceProviderTypeReqDTO deleteServiceProviderTypeReqDTO)
        {

            return _serviceProviderTypeBLL.DeleteServiceProviderType(deleteServiceProviderTypeReqDTO);

        }
    }
}
