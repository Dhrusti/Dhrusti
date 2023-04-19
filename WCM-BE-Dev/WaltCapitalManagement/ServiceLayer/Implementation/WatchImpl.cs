using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class WatchImpl : IWatch
    {
        private readonly WatchBLL _watchBLL;

        public WatchImpl(WatchBLL watchBLL)
        {
            _watchBLL = watchBLL;
        }

        public CommonResponse GetAllGroups(GetAllGroupsReqDTO getAllGroupsReqDTO)
        {
            return _watchBLL.GetAllGroups(getAllGroupsReqDTO);
        }

        public CommonResponse GetAllPrivileges(GetAllPrivilegesReqDTO getAllPrivilegesReqDTO)
        {
            return _watchBLL.GetAllPrivileges(getAllPrivilegesReqDTO);
        }

        public CommonResponse UpdatePrivileges(UpdatePrivilegesReqDTO updatePrivilegesReqDTO)
        {
            return _watchBLL.UpdatePrivileges(updatePrivilegesReqDTO);
        }
        public CommonResponse AddPrivilege(AddPrivilegeReqDTO addPrivilegeReqDTO)
        {
            return _watchBLL.AddPrivilege(addPrivilegeReqDTO);
        }
        public CommonResponse GetAllSelectedPrivileges(GetAllSelectedPrivilegesReqDTO getAllSelectedPrivilegeReqDTO)
        {
            return _watchBLL.GetAllSelectedPrivileges(getAllSelectedPrivilegeReqDTO);
        }
    }
}
