using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class PermissionDTO
	{
		public int Pid { get; set; }
		public string PermissionName { get; set; } = null!;
		public bool? IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
