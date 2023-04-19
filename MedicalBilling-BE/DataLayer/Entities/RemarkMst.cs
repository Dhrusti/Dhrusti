using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class RemarkMst
{
    public int Id { get; set; }

    public decimal? AppointmentId { get; set; }

    public string Remark { get; set; } = null!;

    public string Details { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int? Status { get; set; }
}
