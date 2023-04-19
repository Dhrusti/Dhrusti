using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class PhysicianMst
{
    public int Id { get; set; }

    public string DoctorFirstName { get; set; } = null!;

    public string DoctorLastName { get; set; } = null!;

    public string DoctorDegreeName1 { get; set; } = null!;

    public string DoctorDegreeName2 { get; set; } = null!;

    public string DoctorDegreeName3 { get; set; } = null!;

    public string SecretaryFirstName { get; set; } = null!;

    public string SecretaryLastName { get; set; } = null!;

    public string DoctorEmail { get; set; } = null!;

    public string DoctorMobileNo { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
