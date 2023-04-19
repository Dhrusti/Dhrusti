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
    public class ServiceProviderTypeController : ControllerBase
    {
        private readonly IServiceProviderTypeDetail _iserviceProviderTypeDetail;

        public ServiceProviderTypeController(IServiceProviderTypeDetail serviceProviderTypeDetail)
        {
            _iserviceProviderTypeDetail = serviceProviderTypeDetail;
        }

        [HttpPost("GetAllServiceProviderType")]
        public CommonResponse GetAllServiceProviderType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProviderTypeDetail.GetAllServiceProviderType();
                List<GetAllServiceProviderTypeResDTO> getallserviceProviderTypeResDTO = commonResponse.Data ?? new List<GetAllServiceProviderTypeResDTO>();
                commonResponse.Data = getallserviceProviderTypeResDTO.Adapt<List<GetAllServiceProviderTypeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetByServiceProviderTypeId")]
        public CommonResponse GetByServiceProviderTypeId(GetAllServiceProviderTypeByIdReqViewModel getAllServiceProviderTypeByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProviderTypeDetail.GetByServiceProviderTypeId(getAllServiceProviderTypeByIdReqViewModel.Adapt<GetServiceProviderTypeByIdReqDTO>());
                GetServiceProviderTypeByIdResDTO getServiceProviderTypeByIdResDTO = commonResponse.Data ?? new GetServiceProviderTypeByIdResDTO();
                commonResponse.Data = getServiceProviderTypeByIdResDTO.Adapt<GetServiceProviderTypeByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("AddServiceProviderType")]
        public CommonResponse AddServiceProviderType(AddServiceProviderTypeReqViewModel addServiceProviderTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProviderTypeDetail.AddServiceProviderType(addServiceProviderTypeReqViewModel.Adapt<AddServiceProviderTypeReqDTO>());
                AddServiceProviderTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddServiceProviderTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateServiceProviderType")]
        public CommonResponse UpdateServiceProviderType(UpdateServiceProviderTypeReqViewModel updateServiceProviderTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProviderTypeDetail.UpdateServiceProviderType(updateServiceProviderTypeReqViewModel.Adapt<UpdateServiceProviderTypeReqDTO>());
                UpdateServiceProviderTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateServiceProviderTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteServiceProviderType")]
        public CommonResponse DeleteServiceProviderType(DeleteServiceProviderTypeReqViewModel deleteServiceProviderTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iserviceProviderTypeDetail.DeleteServiceProviderType(deleteServiceProviderTypeReqViewModel.Adapt<DeleteServiceProviderTypeReqDTO>());
                DeleteServiceProviderTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteServiceProviderTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
