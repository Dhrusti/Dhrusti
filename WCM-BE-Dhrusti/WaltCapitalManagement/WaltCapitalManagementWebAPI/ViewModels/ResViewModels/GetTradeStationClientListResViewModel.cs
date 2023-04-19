namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetTradeStationClientListResViewModel
    {
        public List<TradeClientList> TradeClientLists { get; set; }

        public int TotalCount { get; set; }
    }

    public class TradeClientList
    {
        public string ClientName { get; set; }
        public string Account { get; set; }
        public string PortfolioValue { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string PortfolioManager { get; set; }
    }
}
