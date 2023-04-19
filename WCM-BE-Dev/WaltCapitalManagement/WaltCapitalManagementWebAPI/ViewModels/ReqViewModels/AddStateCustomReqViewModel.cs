namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddStateCustomReqViewModel
    {
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public int CreatedBy { get; set; }
    }
}
