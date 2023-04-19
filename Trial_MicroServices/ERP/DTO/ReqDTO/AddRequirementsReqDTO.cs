using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class AddRequirementsReqDTO
	{
		public string MainSkills { get; set; } = null!;

		public int NoOfPosition { get; set; }

		public int TotalMinExp { get; set; }

		public int TotalMaxExp { get; set; }

		public int RelevantMinExp { get; set; }

		public int RelevantMaxExp { get; set; }

		public int TypeofEmployement { get; set; }

		public string Pocname { get; set; } = null!;

		public string MandatorySkill { get; set; } = null!;
	}
}
