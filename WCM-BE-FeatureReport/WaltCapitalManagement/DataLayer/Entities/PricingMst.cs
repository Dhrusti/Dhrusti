using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class PricingMst
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public DateTime? InceptionDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int DynPrcInpFundId { get; set; }
        public string UnitType { get; set; } = null!;
        public decimal DynPrcInpTotal { get; set; }
        public decimal Units { get; set; }
        public decimal Hwm { get; set; }
        public decimal ManFees { get; set; }
        public decimal PerfFees { get; set; }
        public decimal ComplianceFees { get; set; }
        public decimal AuditFees { get; set; }
        public decimal Ifafees { get; set; }
        public decimal UnitPriceNav { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal TotalTheorValue { get; set; }
        public decimal FeesPayable { get; set; }
        public decimal TotalFeesDue { get; set; }
    }
}
