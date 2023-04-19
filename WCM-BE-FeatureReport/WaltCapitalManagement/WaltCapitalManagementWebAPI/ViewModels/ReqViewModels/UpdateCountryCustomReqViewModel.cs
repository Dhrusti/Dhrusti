namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateCountryCustomReqViewModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
