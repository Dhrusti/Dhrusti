using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class DeleteUserResDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; } = null!;
	}
}
