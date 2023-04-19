namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetTranscationTypeByClientIdReqViewModel
    {
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public string UnitType { get; set; }
    }
}
