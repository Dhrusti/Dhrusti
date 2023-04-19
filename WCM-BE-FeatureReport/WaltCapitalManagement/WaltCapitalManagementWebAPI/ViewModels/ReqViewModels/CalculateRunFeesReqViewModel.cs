namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class CalculateRunFeesReqViewModel
    {
        public int FundId { get; set; }
        public int FeesId { get; set; }
        public DateTime NextRunDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
