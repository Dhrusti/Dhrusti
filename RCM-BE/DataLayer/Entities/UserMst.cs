using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class UserMst
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string MobileNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Role { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
