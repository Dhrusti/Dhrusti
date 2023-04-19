namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetPortfolioCSVDataResViewModel
    {
        public List<GetPortfolioCSVData> GetPortfolioCSVDataList { get; set; }
        public GetPortfolioCSVDataTotal getPortfolioCSVDataTotal { get; set; }
        public int TotalCSVCount { get; set; }
    }

    public class GetPortfolioCSVData
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Volume { get; set; }
        public string CostPrice { get; set; }
        public string CurrentPrice { get; set; }
        public string PerChange { get; set; }
        public string CrrentValue { get; set; }
        public string PerPortfolio { get; set; }
    }
    public class GetPortfolioCSVDataTotal
    {
        public string TotalCashCurrentValue { get; set; }
        public string TotalCashPortfolioPercentage { get; set; }
        public string TotalValueOpen { get; set; }
        public string TotalValueNow { get; set; }
        public string TotalPortfolioPercentage { get; set; }
        public string TotalCashCurrentValueString { get; set; }
        public string TotalValueOpenString { get; set; }
        public string TotalValueNowString { get; set; }
    }
}
