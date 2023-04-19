namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddCustomCityReqViewModel
    {

        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int CreatedBy { get; set; }
    }
}
