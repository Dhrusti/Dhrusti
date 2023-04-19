using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class UserAccessPermissionDTO
	{
		public UserMstDTO userMstDTO { get; set; }
		public List<UserPermissionDTO> userPermissionDTO { get; set; }
		public List<AccessDTO> accessDTO { get; set; }
		public List<PermissionDTO> permissionDTO { get; set; }
	}
}
