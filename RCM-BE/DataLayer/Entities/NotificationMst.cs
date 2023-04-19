using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class NotificationMst
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Description { get; set; } = null!;

    public bool IsNotificationRead { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public string AdminDescription { get; set; } = null!;

    public string DescriptionTitle { get; set; } = null!;

    public string AdminDescriptionTitle { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
