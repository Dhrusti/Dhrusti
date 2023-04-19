namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetOfficeByCityIdResViewModel
    {

        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<GetOffice> offices { get; set; }
    }
    public class GetOffice
    {
        public int officeId { get; set; }
        public string officeName { get; set; }
    }
}
