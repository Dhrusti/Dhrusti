namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddServiceProviderTypeReqViewModel
    {
        public int Id { get; set; }
        public string ServiceProviderType { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
