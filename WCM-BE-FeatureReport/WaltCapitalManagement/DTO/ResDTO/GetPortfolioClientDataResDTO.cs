namespace DTO.ResDTO
{
    public class GetPortfolioClientDataResDTO
    {
        public List<GetPortfolioClientData> getPortfolioClientDataList { get; set; }
        public int TotalCount { get; set; }
    }
    public class GetPortfolioClientData
    {
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string BirthDate { get; set; }
        public string JoiningDate { get; set; }
        public string InvestmentValue { get; set; }
        public string StockName { get; set; }
    }
}
