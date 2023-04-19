namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateWaltCapConsultantReqViewModel
    {
        public int Id { get; set; }
        public string WaltCapConsultant { get; set; } = null!;
        public int UserId { get; set; }
    }
}
