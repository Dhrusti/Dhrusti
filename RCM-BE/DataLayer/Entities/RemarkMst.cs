using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class RemarkMst
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public DateTime Datetime { get; set; }

    public string Remark { get; set; } = null!;

    public string Details { get; set; } = null!;

    public int Status { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
