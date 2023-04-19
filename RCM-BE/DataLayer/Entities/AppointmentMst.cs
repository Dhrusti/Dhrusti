using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class AppointmentMst
{
    public int Id { get; set; }

    public int CallTypeId { get; set; }

    public string AccountNo { get; set; } = null!;

    public DateTime? Date { get; set; }

    public DateTime? NewAppoitmentDate { get; set; }

    public DateTime? ActualAppoitmentDate { get; set; }

    public int? ExtensionId { get; set; }

    public string? TaxId { get; set; }

    public string PatientFirstName { get; set; } = null!;

    public string PatientLastName { get; set; } = null!;

    public string PatientEmail { get; set; } = null!;

    public string PatientMobileNo { get; set; } = null!;

    public DateTime PatientDob { get; set; }

    public int? AppDoctorId { get; set; }

    public string? DoctorGender { get; set; }

    public string? Pcp { get; set; }

    public string? PcpmobileNo { get; set; }

    public string? ReferingMd { get; set; }

    public string? ReferingMobileNo { get; set; }

    public string PrimaryInsuranceId { get; set; } = null!;

    public string PrimaryInsuranceName { get; set; } = null!;

    public string? SecondaryInsuranceId { get; set; }

    public string? SecondaryInsuranceName { get; set; }

    public string Notes { get; set; } = null!;

    public string? Reason { get; set; }

    public bool? IsAppoitmentVehicleOrworkInjury { get; set; }

    public bool? IsCovidPossitive { get; set; }

    public string? IsIdCurrentOrExpired { get; set; }

    public bool? IsVaccinated { get; set; }

    public DateTime IdExpirationDate { get; set; }

    public bool? IsMatchInsurance { get; set; }

    public DateTime LastAppoitmentDate { get; set; }

    public string Status { get; set; } = null!;

    public bool IsEditable { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
