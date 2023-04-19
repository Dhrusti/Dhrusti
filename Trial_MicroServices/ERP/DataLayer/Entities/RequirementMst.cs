using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class RequirementMst
{
    public int RequirementId { get; set; }

    public string MainSkills { get; set; } = null!;

    public int NoOfPosition { get; set; }

    public int TotalMinExp { get; set; }

    public int TotalMaxExp { get; set; }

    public int RelevantMinExp { get; set; }

    public int RelevantMaxExp { get; set; }

    public int TypeofEmployement { get; set; }

    public string Pocname { get; set; } = null!;

    public string MandatorySkill { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
