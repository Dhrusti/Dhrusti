namespace DTO.ResDTO
{
    public class MobileGetDashboardResDTO
    {
        public ClientInvestmentAmounts ClientInvestmentAmounts { get; set; }
        public List<ClientInvestmentDetails> ClientInvestmentDetails { get; set; }
    }

    public class ClientInvestmentDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
    public class ClientInvestmentAmounts
    {
        public string PriceInZar { get; set; }
        public string PriceInUsd { get; set; }
        public string PriceInGold { get; set; }
        public string PriceInBitcoin { get; set; }
    }
}
