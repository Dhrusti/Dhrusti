using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class RunFeesDetail
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public int Feesid { get; set; }
        public DateTime LastRunDate { get; set; }
        public decimal LastAmount { get; set; }
        public DateTime? NextRunDate { get; set; }
        public decimal PendingAmount { get; set; }
        public decimal Total { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalIncVat { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
