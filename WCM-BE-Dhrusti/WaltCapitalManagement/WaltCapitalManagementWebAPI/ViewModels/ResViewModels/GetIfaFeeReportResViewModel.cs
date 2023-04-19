namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetIfaFeeReportResViewModel
    {
       public List<FeeSummary> FeeSummaries { get; set; }
        public string Currency { get; set; }
        public string TotalFees { get; set; }
        
    }

    public class FeeSummary
    {
        public string FeeType { get; set; }
        public string Currency { get; set; }
        public string FeeAmount { get; set; }
    }
}
