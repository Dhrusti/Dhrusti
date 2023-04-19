using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class AddUserResDTO
	{
		public string EmployeeCode { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public DateTime Dob { get; set; }

		public string UserName { get; set; } = null!;
	}
}
