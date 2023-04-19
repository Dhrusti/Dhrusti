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
    public class AccessCategoryController : ControllerBase
    {
        private readonly IAccessCategory _iAccessCategory;
        public AccessCategoryController(IAccessCategory iAccessCategory)
        {
            _iAccessCategory = iAccessCategory;
        }

        [HttpPost("AddAccessCategory")]
        public CommonResponse AddAccessCategory(AccessCategoryReqViewModel accessCategoryReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iAccessCategory.AddAccessCategory(accessCategoryReqViewModel.Adapt<AccessCategoryReqDTO>());
                AccessCategoryResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AccessCategoryResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteAccessCategory")]
        public CommonResponse DeleteAccessCategory(DeleteAccessCategoryReqViewModel deleteAccessCategoryReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iAccessCategory.DeleteAccessCategory(deleteAccessCategoryReqViewModel.Adapt<DeleteAccessCategoryReqDTO>());
                DeleteAccessCategoryResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteAccessCategoryResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
