using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTOs;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class AccessPermissionImpl : IAccessPermission
    {
        private readonly AccessPermissionBLL _accesspermissionBLL;

        public AccessPermissionImpl(AccessPermissionBLL accessPermissionBLL)
        {
            _accesspermissionBLL = accessPermissionBLL;
        }

        public ResponseDTO SaveUserPermission(List<UserPermissionDTO> userPermissionDTO)
        {
            ResponseDTO response = new ResponseDTO();
            response = _accesspermissionBLL.SaveUserPermissionBLL(userPermissionDTO);
            return response;
        }
        public ResponseDTO GetPermission()
        {
            return _accesspermissionBLL.GetPermissionBLL();
        }

		public ResponseDTO GetUserById(int id)
		{
			return _accesspermissionBLL.GetUserByIdBLL(id);
		}

        public ResponseDTO UserAccessPermissionbyId(int userid)
        {
            return _accesspermissionBLL.UserAccessPermissionbyIdBLL(userid);
        }

        public ResponseDTO AccessPermissionbyId(int userid)
		{
            return _accesspermissionBLL.AccessPermissionbyIdBLL(userid);

        }


    }
}
