namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateServiceProviderTypeReqViewModel
    {
        public int Id { get; set; }
        public string ServiceProviderType { get; set; } = null!;
        public int UpdateBy { get; set; }
    }
}
