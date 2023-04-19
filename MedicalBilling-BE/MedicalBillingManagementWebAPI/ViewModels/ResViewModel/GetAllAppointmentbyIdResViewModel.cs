namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllAppointmentbyIdResViewModel
    {
        public int CallTypeId { get; set; }

        public decimal? AppointmentNumber { get; set; } = null!;

        public string? Date { get; set; }
        public bool? IsAvailableLastDate { get; set; }

        public int? ExtensionId { get; set; }

        public string? LastAppoitmentDate { get; set; }

        public string? ActualAppoitmentDate { get; set; }

        public string TaxId { get; set; } = null!;

        public string PatientName { get; set; } = null!;

        public string PatientMobileNo { get; set; } = null!;
        public string PatientDob { get; set; } 

        public int? AppDoctorId { get; set; }

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

        public string? IdExpirationDate { get; set; }

        public bool? IsMatchInsurance { get; set; }
        public string? Status { get; set; }


    }
}
