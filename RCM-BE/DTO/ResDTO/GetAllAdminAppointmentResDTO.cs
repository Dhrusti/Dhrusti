using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class GetAllAdminAppointmentResDTO
	{
		public int TotalCount { get; set; }

		public List<AdminAppointment> AdminAppointmentList { get; set; }

		public class AdminAppointment
		{
			public int SR { get; set; }
			public string AccountNo { get; set; } = null!;
			public string PatientName { get; set; }
			public string PrimaryInsuranceName { get; set; }
			public string SecondaryInsuranceName { get; set; }
			public string Status { get; set; } = null!;
			public string? ApptTime { get; set; }
			public string EntryTime { get; set; }
			public string DoName { get; set; }
			public string Notes { get; set; }
			public bool Email { get; set; }
			public bool Remark { get; set; }
			public DateTime? DOB { get; set; }
			public bool IsApprove { get; set; }
			public bool IsReject { get; set; }
			public string ApprovalStatus { get; set; }
			public int ReceptionistId { get; set; }
			public int UserId { get; set; }
			public string CallType { get; set; }
			public DateTime UpdatedDate { get; set; }
			public string PatientEmail { get; set; }

		}
	}
}
