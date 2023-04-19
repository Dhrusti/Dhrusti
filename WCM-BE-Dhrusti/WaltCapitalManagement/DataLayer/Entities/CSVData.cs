namespace DataLayer.Entities
{
    public partial class CSVData
    {
        public int Id { get; set; }
        public string AccountNo { get; set; } = null!;
        public string? Surname { get; set; }
        public string? Category { get; set; }
        public DateTime? InvDate { get; set; }
        public string? Share { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
        public double PercentTot { get; set; }
        public int CsvfileId { get; set; }
    }
}
