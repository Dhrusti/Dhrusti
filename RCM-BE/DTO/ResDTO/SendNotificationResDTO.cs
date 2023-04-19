using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class SendNotificationResDTO
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Description { get; set; }
	}
}
