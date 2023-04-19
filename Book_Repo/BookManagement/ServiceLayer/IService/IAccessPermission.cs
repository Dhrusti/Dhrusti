using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace ServiceLayer.IService
{
	public interface IAccessPermission
	{

		public ResponseDTO SaveUserPermission(List<UserPermissionDTO> userPermissionDTO);

		public ResponseDTO GetUserById(int id);
		public ResponseDTO GetPermission();

		public ResponseDTO UserAccessPermissionbyId(int userid);

		public ResponseDTO AccessPermissionbyId(int userid);
	}
}
