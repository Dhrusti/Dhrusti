namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetTransactionUnitTypeByClientIdReqViewModel
    {
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public string UniType { get; set; }

    }
}
