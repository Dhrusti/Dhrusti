using System;
using System.Collections.Generic;

namespace ProactAccouting.Models;

public partial class LevelSecondMst
{
    public int LevelSecondId { get; set; }

    public string? Code { get; set; }

    public string? CodeName { get; set; }

    public int? LevelFirstId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual LevelFirstMst? LevelFirst { get; set; }

    public virtual ICollection<LevelThirdMst> LevelThirdMsts { get; } = new List<LevelThirdMst>();
}
