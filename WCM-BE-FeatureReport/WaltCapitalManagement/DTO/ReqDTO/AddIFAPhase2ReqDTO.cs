namespace DTO.ReqDTO
{
    public class AddIFAPhase2ReqDTO
    {
        public int Id { get; set; }
        public string? InitialFee { get; set; }
        public string? AnnualAdvisorFees { get; set; }
        public string? PerformanceFee { get; set; }
        public string? Other { get; set; }
        public bool IsVatapplicable { get; set; }
        public int CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
