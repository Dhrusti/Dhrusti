namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllClientListResViewModel
    {
        public int UserId { get; set; }
        public string ClientAccNo { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
