namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateOfficeReqViewModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Office { get; set; } = null!;
        public int UserId { get; set; }
    }
}
