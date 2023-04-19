using System;
using System.Collections.Generic;

namespace ProactAccouting.Models;

public partial class LevelFifthMst
{
    public int LevelFifthId { get; set; }

    public string? Code { get; set; }

    public string? CodeName { get; set; }

    public int? LevelFourthId { get; set; }

    public bool IsFinalLevel { get; set; }

    public string? CreditDebit { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual LevelFourthMst? LevelFourth { get; set; }
}
