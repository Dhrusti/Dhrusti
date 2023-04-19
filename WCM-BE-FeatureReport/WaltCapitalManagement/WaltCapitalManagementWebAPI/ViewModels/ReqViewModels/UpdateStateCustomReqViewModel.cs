namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateStateCustomReqViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
