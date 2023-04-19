namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllClientResViewModel
    {
        public int TotalCount { get; set; }
        public List<ClientDetails> ClientList { get; set; }
    }
    public class ClientDetails
    {
        public int UserId { get; set; }
        public string ClientAccNo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string InvestmentValue { get; set; } = "0";
        public string? Status { get; set; }
        public DateTime Dob { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
