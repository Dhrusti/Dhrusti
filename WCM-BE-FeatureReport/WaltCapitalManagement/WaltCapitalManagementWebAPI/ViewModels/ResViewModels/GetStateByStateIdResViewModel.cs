namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetStateByStateIdResViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
    }
}
