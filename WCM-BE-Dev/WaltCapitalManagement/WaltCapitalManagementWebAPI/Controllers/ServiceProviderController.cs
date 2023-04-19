using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IServiceProviderDetails _iserviceProvider;

        public ServiceProviderController(IServiceProviderDetails iserviceProvider)
        {
            _iserviceProvider = iserviceProvider;
        }

        [HttpPost("GetAllServiceProvider")]
        public CommonResponse GetAllServiceProvider()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProvider.GetAllServiceProvider();
                List<GetAllServiceProviderResDTO> ServiceProviderResDTO = commonResponse.Data ?? new List<GetAllServiceProviderResDTO>();
                commonResponse.Data = ServiceProviderResDTO.Adapt<List<GetAllServiceProviderResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetByServiceProviderId")]
        public CommonResponse GetByServiceProviderId(GetAllServiceProviderByIdReqViewModel getAllServiceProviderByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProvider.GetByServiceProviderId(getAllServiceProviderByIdReqViewModel.Adapt<GetServiceProviderByIdReqDTO>());
                GetServiceProviderByIdResDTO serviceProviderbyIdResDTO = commonResponse.Data ?? new GetServiceProviderByIdResDTO();
                commonResponse.Data = serviceProviderbyIdResDTO.Adapt<GetServiceProviderByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddServiceProvider")]
        public CommonResponse AddServiceProvider(AddServiceProviderReqViewModel addServiceProviderReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProvider.AddServiceProvider(addServiceProviderReqViewModel.Adapt<AddServiceProviderReqDTO>());
                AddServiceProviderResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddServiceProviderResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateServiceProvider")]
        public CommonResponse UpdateServiceProvider(UpdateServiceProviderReqViewModel updateServiceProviderReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProvider.UpdateServiceProvider(updateServiceProviderReqViewModel.Adapt<UpdateServiceProviderReqDTO>());
                UpdateServiceProviderResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateServiceProviderResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteServiceProvider")]
        public CommonResponse DeleteServiceProvider(DeleteServiceProviderReqViewModel deleteServiceProviderReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProvider.DeleteServiceProvider(deleteServiceProviderReqViewModel.Adapt<DeleteServiceProviderReqDTO>());
                DeleteServiceProviderResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteServiceProviderResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
