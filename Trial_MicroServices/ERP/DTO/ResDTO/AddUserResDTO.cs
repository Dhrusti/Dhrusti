using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class AddUserResDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; } = null!;
		public string FullName { get; set; } = null!;
	}
}
