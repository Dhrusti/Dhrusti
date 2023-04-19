namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetOffshoreClientResViewModel
    {
        public List<OffShoreClient> offShoreClientLists { get; set; }
        public OffShoreClient offShoreClient { get; set; }
        public int TotalCount { get; set; }

    }

    public class OffShoreClient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AccountNo { get; set; }
        public string Currency { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public double TransactionAmount { get; set; }
        public int Client { get; set; }
        public decimal? AccountValue { get; set; }
        public decimal? ClientTotalValue { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
