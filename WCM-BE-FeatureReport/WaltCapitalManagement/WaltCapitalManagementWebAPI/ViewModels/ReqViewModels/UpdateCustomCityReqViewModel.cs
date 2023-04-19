namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateCustomCityReqViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
