namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateExternalAccountReqViewModel
    {
        public int Id { get; set; }
        public int ServiceProvider { get; set; }
        public int Type { get; set; }
        public string AccountCode { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
