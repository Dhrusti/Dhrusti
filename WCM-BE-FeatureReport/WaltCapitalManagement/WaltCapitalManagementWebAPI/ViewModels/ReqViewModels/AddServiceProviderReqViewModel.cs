namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddServiceProviderReqViewModel
    {
        public int Id { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
