using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class AddUserReqDTO
	{
		public string EmployeeCode { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public int Gender { get; set; }

		public string Email { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public string EmergencyContact { get; set; } = null!;

		public DateTime Dob { get; set; }

		public string UserName { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string ConfirmPassword { get; set; } = null!;

		public string PermanentAddress { get; set; } = null!;

		public string CurrentAddress { get; set; } = null!;

		public string PostCode { get; set; } = null!;

		public int EmploymentType { get; set; }

		public int CompanyName { get; set; }

		public string Department { get; set; } = null!;

		public string Designation { get; set; } = null!;

		public string Location { get; set; } = null!;

		public string BloodGroup { get; set; } = null!;

		public DateTime OfferDate { get; set; }

		public DateTime JoinDate { get; set; }

		public int Role { get; set; }

		public string BankName { get; set; } = null!;

		public string AccountNumber { get; set; } = null!;

		public string Branch { get; set; } = null!;

		public string Ifsccode { get; set; } = null!;

		public string PfaccountNumber { get; set; } = null!;

		public string PancardNumber { get; set; } = null!;

		public string AdharCardNumber { get; set; } = null!;

		public string Salary { get; set; } = null!;

		public int ReportingManager { get; set; }

		public string Reason { get; set; } = null!;

		public string EmployeePersonalEmailId { get; set; } = null!;

		public string ProbationPeriod { get; set; } = null!;

	}
}
