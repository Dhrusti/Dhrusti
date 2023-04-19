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
    public class AccessCategoryTypeController : ControllerBase
    {
        private readonly IAccessCategoryType _iaccessCategoryType;
        public AccessCategoryTypeController(IAccessCategoryType iaccessCategoryType)
        {
            _iaccessCategoryType = iaccessCategoryType;
        }

        [HttpPost("AddAccessCategoryType")]
        public CommonResponse AddAccessCategoryType(AccessCategoryTypeReqViewModel accessCategoryTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccessCategoryType.AddAccessCategoryType(accessCategoryTypeReqViewModel.Adapt<AccessCategoryTypeReqDTO>());
                AccessCategoryTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddWaltCapConsultantResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteAccessCategoryType")]
        public CommonResponse DeleteAccessCategoryType(DeleteAccessCategoryTypeReqViewModel deleteAccessCategoryTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccessCategoryType.DeleteAccessCategoryType(deleteAccessCategoryTypeReqViewModel.Adapt<DeleteAccessCategoryTypeReqDTO>());
                DeleteAccessCategoryTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteAccessCategoryResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
