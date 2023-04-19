namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddClientTypeReqViewModel
    {
        public int Id { get; set; }
        public string ClientType { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
