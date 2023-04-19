using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ExternalAccountDetail
    {
        public int Id { get; set; }
        public int ServiceProvider { get; set; }
        public int Type { get; set; }
        public string AccountCode { get; set; } = null!;
        public int ClientId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
