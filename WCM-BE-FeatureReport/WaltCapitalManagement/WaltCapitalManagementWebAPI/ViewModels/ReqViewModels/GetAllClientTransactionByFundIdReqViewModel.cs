namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetAllClientTransactionByFundIdReqViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }

        public int FundId { get; set; }
        public string UnitType { get; set; }
    }
}
