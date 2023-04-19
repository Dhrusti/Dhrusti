namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateServiceProviderReqViewModel
    {
        public int Id { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
