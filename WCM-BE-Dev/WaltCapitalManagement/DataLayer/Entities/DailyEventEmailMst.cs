using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class DailyEventEmailMst
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Gender { get; set; } = null!;
        public string EmailFor { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string UploadTemplate { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
