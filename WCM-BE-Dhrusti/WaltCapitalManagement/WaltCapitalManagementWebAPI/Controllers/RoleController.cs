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
    public class RoleController : ControllerBase
    {
        private readonly IRole _iRole;
        public RoleController(IRole iRole)
        {
            _iRole = iRole;
        }
        [HttpPost("GetAllRole")]
        public CommonResponse GetAllRole(GetRoleReqViewModel getRoleReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRole.GetAllRole(getRoleReqViewModel.Adapt<GetRoleReqDTO>());
                GetAllRoleResDTO getRoleResDTO = commonResponse.Data ?? new GetAllRoleResDTO();
                commonResponse.Data = getRoleResDTO.Adapt<GetAllRoleResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetRoleById")]
        public CommonResponse GetRoleById(GetRoleByIdReqViewModel getRoleByIdReqView)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRole.GetRoleById(getRoleByIdReqView.Adapt<GetRoleByIdReqDTO>());
                GetRoleByIdResDTO getRoleByResDTO = commonResponse.Data ?? new GetRoleByIdResDTO();
                commonResponse.Data = getRoleByResDTO.Adapt<GetRoleByResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddRole")]
        public CommonResponse AddRole(AddRoleReqViewModel addRoleReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRole.AddRole(addRoleReqViewModel.Adapt<AddRoleReqDTO>());
                AddRoleResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddRoleResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateRole")]
        public CommonResponse UpdateRole(UpdateRoleReqViewModel updateRoleReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRole.UpdateRole(updateRoleReqViewModel.Adapt<UpdateRoleReqDTO>());
                UpdateRoleResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateRoleResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        [HttpPost("UpdateRoleStatus")]
        public CommonResponse UpdateRoleStatus(UpdateRoleStatusReqViewModel updateRoleStatucReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iRole.UpdateRoleStatus(updateRoleStatucReqViewModel.Adapt<UpdateRoleStatusReqDTO>());
                UpdateRoleStatusResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateRoleStatusResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
