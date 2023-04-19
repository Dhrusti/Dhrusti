using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblSubCategoryMst
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
