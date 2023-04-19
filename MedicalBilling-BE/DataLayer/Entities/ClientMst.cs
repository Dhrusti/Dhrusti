using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class ClientMst
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string OfficeName { get; set; } = null!;

    public int Country { get; set; }

    public string? StreetNo { get; set; }

    public string? HomeName { get; set; }

    public string? StreetName { get; set; }

    public string? Suburb { get; set; }

    public string? City { get; set; }

    public int Province { get; set; }

    public string? PostalCode { get; set; }

    public string InfoEmail { get; set; } = null!;

    public string AppoitmentEmail { get; set; } = null!;

    public string DoctorEmail { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string FaxNo { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
