using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class RegistrationReqDTO
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string MobileNo { get; set; } = null!;

		public int Designation { get; set; }

		public string Address { get; set; } = null!;
	}
}
