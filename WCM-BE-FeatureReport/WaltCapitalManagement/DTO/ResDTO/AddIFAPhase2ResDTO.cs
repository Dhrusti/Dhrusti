namespace DTO.ResDTO
{
    public class AddIFAPhase2ResDTO
    {
        public string? InitialFee { get; set; }
        public string? AnnualAdvisorFees { get; set; }
        public string? PerformanceFee { get; set; }
        public string? Other { get; set; }
        public bool IsVatapplicable { get; set; }
        public bool? IsActive { get; set; }
    }
}
