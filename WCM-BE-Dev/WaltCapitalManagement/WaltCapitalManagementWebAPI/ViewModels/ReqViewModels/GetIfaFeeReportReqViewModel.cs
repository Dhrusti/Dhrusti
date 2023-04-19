namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetIfaFeeReportReqViewModel
    {
        public int FundId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? FilterBy { get; set; }
    }
}

