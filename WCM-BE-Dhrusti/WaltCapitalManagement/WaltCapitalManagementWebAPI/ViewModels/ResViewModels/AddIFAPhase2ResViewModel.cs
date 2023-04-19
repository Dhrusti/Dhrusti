namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class AddIFAPhase2ResViewModel
    {
        public string? InitialFee { get; set; }
        public string? AnnualAdvisorFees { get; set; }
        public string? PerformanceFee { get; set; }
        public string? Other { get; set; }
        public bool IsVatapplicable { get; set; }
        public bool? IsActive { get; set; }
    }
}
