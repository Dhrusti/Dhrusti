namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class PortFolioDataResViewModel
    {
        public List<PortfolioServiceProvider> PortfolioServiceProviders { get; set; }
        public PortfolioClientDetail portfolioClientDetail { get; set; }
        public PortfolioClientInvestmentDetail portfolioClientInvestmentDetail { get; set; }
    }

    public class PortfolioServiceProvider
    {
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public int ServiceProviderTypeId { get; set; }
        public string ServiceProviderTypeName { get; set; }
        public string CurrencyShortName { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountString { get; set; }
        public int ClientCount { get; set; }
        public decimal InvestedPercentage { get; set; }
        public string InvestedPercentageString { get; set; }
    }
    public class PortfolioClientDetail
    {
        public int ClientId { get; set; }
        public string PhotoPath { get; set; }
        public string ClientName { get; set; }
        public string AccountNo { get; set; }
        public string BirthDate { get; set; }
        public string JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class PortfolioClientInvestmentDetail
    {
        public string PriceInZar { get; set; }
        public string PriceInUsd { get; set; }
        public string PriceInGold { get; set; }
        public string PriceInBitcoin { get; set; }
    }
}
