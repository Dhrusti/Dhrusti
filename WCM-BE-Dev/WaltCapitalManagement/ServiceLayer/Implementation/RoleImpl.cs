using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class RoleImpl : IRole
    {
        private readonly RoleBLL _roleBLL;
        public RoleImpl(RoleBLL roleBLL)
        {
            _roleBLL = roleBLL;
        }

        public CommonResponse GetAllRole(GetRoleReqDTO getRoleReqDTO)
        {
            return _roleBLL.GetAllRole(getRoleReqDTO);
        }
        public CommonResponse GetRoleById(GetRoleByIdReqDTO getRoleByIdReqDTO)
        {
            return _roleBLL.GetRoleById(getRoleByIdReqDTO);
        }

        public CommonResponse AddRole(AddRoleReqDTO getIFAReqDTO)
        {
            return _roleBLL.AddRole(getIFAReqDTO);
        }
        public CommonResponse UpdateRole(UpdateRoleReqDTO updateRoleReqDTO)
        {
            return _roleBLL.UpdateRole(updateRoleReqDTO);
        }
        public CommonResponse UpdateRoleStatus(UpdateRoleStatusReqDTO updateRoleStatusReqDTO)
        {
            return _roleBLL.UpdateRoleStatus(updateRoleStatusReqDTO);
        }

    }
}
