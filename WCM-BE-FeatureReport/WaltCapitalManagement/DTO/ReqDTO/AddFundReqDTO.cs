namespace DTO.ReqDTO
{
    public class AddFundReqDTO
    {
        public int FundRiskRating { get; set; }
        public bool IsVatapplicable { get; set; }
        public double? Vat { get; set; }
        public string FundName { get; set; } = null!;
        public string FundPhilosophy { get; set; } = null!;
        public string PricingInputs { get; set; } = null!;
        public DateTime InceptionDate { get; set; }
        public double UnitStartingPrice { get; set; }
        public double ManagementFeeA { get; set; }
        public double ManagementFeeB { get; set; }
        public double PerformanceFeeA { get; set; }
        public double PerformanceFeeB { get; set; }
        public double ComplianceFee { get; set; }
        public double TrusteesFee { get; set; }
        public string Currency { get; set; } = null!;
        public double AuditFee { get; set; }
        public int CreatedBy { get; set; }
        public List<FundDynamicField> FundDynamicField { get; set; }
    }
    public class FundDynamicField
    {
        public int RowId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
