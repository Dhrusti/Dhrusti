namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetExternalAccountResViewModel
    {
        public int Id { get; set; }
        public int ServiceProvider { get; set; }
        public string ServiceProviderName { get; set; }
        public int ServiceProviderType { get; set; }
        public string ServiceProviderTypeName { get; set; }
        public string AccountCode { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
