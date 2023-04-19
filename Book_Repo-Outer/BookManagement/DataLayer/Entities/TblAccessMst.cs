using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblAccessMst
    {
        public int AccessId { get; set; }
        public string AccessName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
