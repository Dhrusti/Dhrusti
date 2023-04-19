using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FundMst
    {
        public int Id { get; set; }
        public int FundRiskRating { get; set; }
        public bool IsVatapplicable { get; set; }
        public double Vat { get; set; }
        public string FundName { get; set; } = null!;
        public string FundPhilosophy { get; set; } = null!;
        public string PricingInputs { get; set; } = null!;
        public DateTime InceptionDate { get; set; }
        public double UnitStartingPrice { get; set; }
        public double ManagementFeeA { get; set; }
        public double ManagementFeeB { get; set; }
        public double ManagementFeeC { get; set; }
        public double PerformanceFeeA { get; set; }
        public double PerformanceFeeB { get; set; }
        public double PerformanceFeeC { get; set; }
        public double AuditFee { get; set; }
        public string Currency { get; set; } = null!;
        public double ComplianceFee { get; set; }
        public double TrusteesFee { get; set; }
        public bool IsFactSheetCreated { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
