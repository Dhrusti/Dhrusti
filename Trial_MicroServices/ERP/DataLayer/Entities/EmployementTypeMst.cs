using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class EmployementTypeMst
{
    public int EmployementTypeId { get; set; }

    public string EmployementType { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
