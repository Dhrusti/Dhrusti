using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class AccessCategoryMst
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; } = null!;
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
