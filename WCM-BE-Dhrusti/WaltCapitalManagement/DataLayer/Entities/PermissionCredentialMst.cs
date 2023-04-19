using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class PermissionCredentialMst
    {
        public int Id { get; set; }
        public int AccessCategoryId { get; set; }
        public string Pwd { get; set; } = null!;
    }
}
