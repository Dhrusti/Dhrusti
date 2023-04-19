using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblCategoryMst
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
