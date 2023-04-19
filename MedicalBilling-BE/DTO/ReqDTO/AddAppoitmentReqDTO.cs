using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddAppoitmentReqDTO
    {
        public int CallTypeId { get; set; }

        public decimal? AppointmentNumber { get; set; } = null!;

        public DateTime Date { get; set; }
        public  bool? IsAvailableLastDate { get; set; } 

        public int ExtensionId { get; set; }

        public DateTime? LastAppoitmentDate { get; set; } = null!;

        public string ActualAppoitmentDate { get; set; } 

        public string TaxId { get; set; } = null!;

        public string  PatientName { get; set; } = null!;

        public string PatientMobileNo { get; set; } = null!;
        public DateTime PatientDob { get; set; } = DateTime.Now;

        public int AppDoctorId { get; set; }

        public string DoctorGender { get; set; } = null!;

        public string Pcp { get; set; } = null!;

        public string PcpmobileNo { get; set; } = null!;

        public string ReferingMd { get; set; } = null!;

        public string ReferingMobileNo { get; set; } = null!;

        public string PrimaryInsuranceId { get; set; } = null!;

        public string PrimaryInsuranceName { get; set; } = null!;

        public string SecondaryInsuranceId { get; set; } = null!;

        public string SecondaryInsuranceName { get; set; } = null!;

        public string Notes { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public bool? IsAppoitmentVehicleOrworkInjury { get; set; }
        public string PatientEmail { get; set; } = null!;


        public bool? IsCovidPossitive { get; set; }

        public string? IsIdCurrentOrExpired { get; set; } = null!;

        public bool? IsVaccinated { get; set; }

        public DateTime IdExpirationDate { get; set; } 

        public bool? IsMatchInsurance { get; set; }
        public int CreatedBy { get; set; }
        public string? Status { get; set; }

    }
}
