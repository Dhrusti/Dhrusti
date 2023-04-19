using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class UserPermissionDTO
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Permissionid { get; set; }
        public int AccessId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
