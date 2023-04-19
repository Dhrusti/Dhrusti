namespace DTO.ResDTO
{
    public class GetPortfolioManagerFeeResDTO
    {
        public List<PortfolioDetails> PortfolioDetails { get; set; }
        public int TotalCount { get; set; }

    }
    public class PortfolioDetails
    {
        public string PortfolioManager { get; set; }
        public decimal Managementfeeslocal { get; set; }
        public decimal ManagementfeesOffshore { get; set; }
        public decimal Performancefees { get; set; }
        public decimal PreffeesOffshore { get; set; }
        public decimal Minfees { get; set; }
        public decimal Total { get; set; }
        public decimal VAT { get; set; }
    }

}
