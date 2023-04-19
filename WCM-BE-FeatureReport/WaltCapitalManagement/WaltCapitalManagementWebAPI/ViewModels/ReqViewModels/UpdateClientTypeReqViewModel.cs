namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateClientTypeReqViewModel
    {
        public int Id { get; set; }
        public string ClientType { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
