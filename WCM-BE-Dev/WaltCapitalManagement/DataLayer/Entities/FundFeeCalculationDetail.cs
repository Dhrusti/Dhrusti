using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FundFeeCalculationDetail
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public DateTime BalanceDate { get; set; }
        public int DynPrcInpId { get; set; }
        public string DynPrcInpLabel { get; set; } = null!;
        public string UnitType { get; set; } = null!;
        public decimal StartValue { get; set; }
        public decimal FeesPayable { get; set; }
        public decimal FeesPaid { get; set; }
        public decimal TotalFeesDue { get; set; }
        public decimal Hwm { get; set; }
        public decimal ProfitOffHwm { get; set; }
        public decimal PreUnits { get; set; }
        public decimal UnitChanges { get; set; }
        public decimal PostUnits { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal BankBalance { get; set; }
        public decimal TstotalValue { get; set; }
        public decimal TstheoValue { get; set; }
        public decimal TotalTheorValue { get; set; }
        public decimal ManFees { get; set; }
        public decimal PerfFees { get; set; }
        public decimal ComplianceFees { get; set; }
        public decimal AuditFees { get; set; }
        public decimal IfainitialFee { get; set; }
        public decimal IfaannualFee { get; set; }
        public decimal Ifafees { get; set; }
        public decimal UnitPriceNav { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
