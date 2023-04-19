using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblBookDownloadMst
    {
        public int BookUserId { get; set; }
        public int BookId { get; set; }
        public string? FirstName { get; set; }
        public string? LastNane { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }
        public string? Location { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
