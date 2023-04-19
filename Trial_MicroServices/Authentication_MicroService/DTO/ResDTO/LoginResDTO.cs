using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class LoginResDTO
	{
		public int TokenId { get; set; }

		//public int UserId { get; set; }

		public string Token { get; set; } = null!;

		public string RefreshToken { get; set; } = null!;

		public DateTime TokenCreated { get; set; }

		public DateTime TokenExpires { get; set; }
	}
}
