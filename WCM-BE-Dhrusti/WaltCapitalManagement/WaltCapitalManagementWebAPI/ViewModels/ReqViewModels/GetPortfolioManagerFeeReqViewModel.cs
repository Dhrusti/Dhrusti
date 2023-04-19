namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetPortfolioManagerFeeReqViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
        public DateTime EndDate { get; set; }
    }
}
