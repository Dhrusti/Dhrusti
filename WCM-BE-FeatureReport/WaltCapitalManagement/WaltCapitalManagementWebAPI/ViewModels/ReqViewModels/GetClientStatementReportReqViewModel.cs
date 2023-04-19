namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetClientStatementReportReqViewModel
    {
        public string ClientAccNo { get; set; }
        public int FundId { get; set; }
        public string ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
