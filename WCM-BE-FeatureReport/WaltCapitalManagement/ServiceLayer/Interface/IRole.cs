using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IRole
    {
        public CommonResponse GetAllRole(GetRoleReqDTO getRoleReqDTO);
        public CommonResponse GetRoleById(GetRoleByIdReqDTO getRoleByIdReqDTO);

        public CommonResponse AddRole(AddRoleReqDTO addRoleReqDTO);
        public CommonResponse UpdateRole(UpdateRoleReqDTO updateRoleReqDTO);
        public CommonResponse UpdateRoleStatus(UpdateRoleStatusReqDTO updateRoleStatusReqDTO);
    }
}
