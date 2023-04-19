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
    public class WatchController : ControllerBase
    {
        private readonly IWatch _iWatch;

        public WatchController(IWatch iWatch)
        {
            _iWatch = iWatch;
        }

        [HttpPost("UpdatePrivileges")]
        public CommonResponse UpdatePrivileges(UpdatePrivilegesReqModel updatePrivilegesReqModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iWatch.UpdatePrivileges(updatePrivilegesReqModel.Adapt<UpdatePrivilegesReqDTO>());
                List<GetAllGroupsResDTO> getAllGroupsResDTO = commonResponse.Data ?? new List<GetAllGroupsResDTO>();
                commonResponse.Data = getAllGroupsResDTO.Adapt<List<GetAllGroupsResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllGroups")]
        public CommonResponse GetAllGroups(GetAllGroupsReqViewModel getAllGroupsReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iWatch.GetAllGroups(getAllGroupsReqViewModel.Adapt<GetAllGroupsReqDTO>());
                List<GetAllGroupsResDTO> getAllGroupsResDTO = commonResponse.Data ?? new List<GetAllGroupsResDTO>();
                commonResponse.Data = getAllGroupsResDTO.Adapt<List<GetAllGroupsResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllPrivileges")]
        public CommonResponse GetAllPrivileges(GetAllPrivilegesReqDTO getAllPrivilegesReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iWatch.GetAllPrivileges(getAllPrivilegesReqDTO.Adapt<GetAllPrivilegesReqDTO>());
                List<GetAllPrivilegesResDTO> getAllPrivilegesResDTO = commonResponse.Data ?? new List<GetAllGroupsResDTO>();
                commonResponse.Data = getAllPrivilegesResDTO.Adapt<List<GetAllPrivilegesResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddPrivilege")]
        public CommonResponse AddPrivilege(AddPrivilegeReqViewModel addPrivilegeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iWatch.AddPrivilege(addPrivilegeReqViewModel.Adapt<AddPrivilegeReqDTO>());
                List<GetAllPrivilegesResDTO> getAllPrivilegesResDTO = commonResponse.Data ?? new List<GetAllGroupsResDTO>();
                commonResponse.Data = getAllPrivilegesResDTO.Adapt<List<GetAllPrivilegesResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllSelectedPrivileges")]
        public CommonResponse GetAllSelectedPrivileges(GetAllSelectedPrivilegesReqViewModel getAllSelectedPrivilegesReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iWatch.GetAllSelectedPrivileges(getAllSelectedPrivilegesReqViewModel.Adapt<GetAllSelectedPrivilegesReqDTO>());
                GetAllSelectedPrivilegesResDTO getAllSelectedPrivilegesResDTO = commonResponse.Data ?? new GetAllSelectedPrivilegesResDTO();
                commonResponse.Data = getAllSelectedPrivilegesResDTO.Adapt<GetAllSelectedPrivilegesResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
