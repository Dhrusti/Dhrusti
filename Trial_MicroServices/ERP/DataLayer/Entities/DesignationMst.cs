using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class DesignationMst
{
    public int DesignationId { get; set; }

    public string Designation { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
