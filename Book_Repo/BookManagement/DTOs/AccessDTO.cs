using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public class AccessDTO
	{
		public int AccessId { get; set; }
		public string AccessName { get; set; } = null!;
		public bool? IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
