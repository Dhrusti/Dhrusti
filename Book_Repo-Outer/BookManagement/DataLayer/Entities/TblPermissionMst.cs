using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblPermissionMst
    {
        public int Pid { get; set; }
        public string PermissionName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
