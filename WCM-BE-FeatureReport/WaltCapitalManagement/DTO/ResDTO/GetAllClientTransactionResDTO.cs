namespace DTO.ResDTO
{
    public class GetAllClientTransactionResDTO
    {
        public List<ClientTransactionDetails> ClientTransactionList { get; set; }
        public int TotalCount { get; set; }

    }

    public class ClientTransactionDetails
    {

        public int? FundId { get; set; }
        public string InvestorNo { get; set; }
        public int TransactionNo { get; set; }
        public string FundName { get; set; } = null!;
        public int Client { get; set; }
        public string ClientName { get; set; } = null!;
        public int Ifa { get; set; }
        public string IFAName { get; set; } = null!;
        public double IfaupFrontFee { get; set; }
        public double IfaAnnualFee { get; set; }
        public string Buy { get; set; } = null!;
        public string Sell { get; set; } = null!;
        public string TransactionIn { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public double NumberOfUnits { get; set; }
        public double UnitPrice { get; set; }
        public string UnitType { get; set; }
        public string AllocateTo { get; set; } = null!;
        public bool IsDeleteIcon { get; set; }

    }
}
