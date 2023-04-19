namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllStateCustomResViewModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string? PhoneCode { get; set; }
        public string? Iso2 { get; set; }
        public List<StateList> stateList { get; set; }

    }
    public class StateList
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
