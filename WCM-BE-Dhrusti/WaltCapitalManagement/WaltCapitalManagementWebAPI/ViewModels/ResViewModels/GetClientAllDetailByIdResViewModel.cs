namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetClientAllDetailByIdResViewModel
    {
        public List<ClientPortfolioServiceProvider> ClientPortfolioServiceProviders { get; set; }
        public ClientDetail ClientDetail { get; set; }
        public ClientPortfolioInvestmentDetail ClientPortfolioInvestmentDetail { get; set; }
    }

    public class ClientPortfolioServiceProvider
    {
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public int ServiceProviderTypeId { get; set; }
        public string ServiceProviderTypeName { get; set; }
        public string CurrencyShortName { get; set; }
        public double TotalAmount { get; set; }
        public string TotalAmountString { get; set; }
        public int ClientCount { get; set; }
        public double InvestedPercentage { get; set; }
        public string InvestedPercentageString { get; set; }
    }
    public class ClientDetail
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

    public class ClientPortfolioInvestmentDetail
    {
        public string PriceInZar { get; set; }
        public string PriceInUsd { get; set; }
        public string PriceInGold { get; set; }
        public string PriceInBitcoin { get; set; }
    }
}
