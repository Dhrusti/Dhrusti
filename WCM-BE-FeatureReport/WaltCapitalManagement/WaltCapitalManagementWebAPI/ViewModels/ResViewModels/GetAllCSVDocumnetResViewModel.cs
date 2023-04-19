namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllCSVDocumnetResViewModel
    {
        public int TotalCount { get; set; }
        public List<CSVDocumentDetails> CSVDocumentList { get; set; }
    }
    public class CSVDocumentDetails
    {
        public string AccountNo { get; set; } = null!;
        public string? Surname { get; set; }
        public string? Category { get; set; }
        public DateTime InvDate { get; set; }
        public string? Share { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
        public double PercentTot { get; set; }
    }
}
