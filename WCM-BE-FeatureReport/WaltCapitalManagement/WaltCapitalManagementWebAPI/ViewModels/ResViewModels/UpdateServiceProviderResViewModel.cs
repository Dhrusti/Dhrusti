namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class UpdateServiceProviderResViewModel
    {
        public int Id { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
