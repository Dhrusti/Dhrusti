using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IWatch
    {
        public CommonResponse GetAllGroups(GetAllGroupsReqDTO getAllGroupsReqDTO);

        public CommonResponse GetAllPrivileges(GetAllPrivilegesReqDTO getAllPrivilegesReqDTO);

        public CommonResponse UpdatePrivileges(UpdatePrivilegesReqDTO updatePrivilegesReqDTO);

        public CommonResponse AddPrivilege(AddPrivilegeReqDTO addPrivilegeReqDTO);

        public CommonResponse GetAllSelectedPrivileges(GetAllSelectedPrivilegesReqDTO getAllSelectedPrivilegeReqDTO);
    }
}
