namespace DTO.ReqDTO
{
    public class AddClientTransactionReqDTO
    {
        public int Fund { get; set; }
        public int Client { get; set; }
        public int Ifa { get; set; }
        public double IfaupFrontFee { get; set; }
        public double IfaAnnualFee { get; set; }
        public string TransactionType { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public double NumberOfUnits { get; set; }
        public double UnitPrice { get; set; }
        public string AllocateTo { get; set; } = null!;
        public string? UnitType { get; set; }
        public int CreatedBy { get; set; }
    }
}
