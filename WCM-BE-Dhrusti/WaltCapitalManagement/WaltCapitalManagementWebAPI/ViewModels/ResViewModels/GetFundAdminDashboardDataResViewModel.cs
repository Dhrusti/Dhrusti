namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetFundAdminDashboardDataResViewModel
    {
        public FundAdminDashboardGraphData fundAdminDashboardGraphData { get; set; }
        public List<FundAdminDashboardReturnsData> fundAdminDashboardReturnsData { get; set; }
        public List<FundAdminDashboardFundStatsData> fundAdminDashboardFundStatsData { get; set; }
        public FundAdminDashboardCommentryData fundAdminDashboardCommentryData { get; set; }
    }
    public class FundAdminDashboardGraphData
    {
        public string FundPhilosophy { get; set; }
        public List<FundGraphData> fundGraphData { get; set; }
    }

    public class FundGraphData
    {
        public string FundName { get; set; }
        public string FundBenchMarkName { get; set; }
        public decimal FundUnitPrice { get; set; }
        public decimal FundBenchmarkUnitPrice { get; set; }
        public string Date { get; set; }

    }
    public class FundAdminDashboardReturnsData
    {
        public string Title { get; set; }
        public string values { get; set; }
    }
    public class FundAdminDashboardFundStatsData
    {
        public string Title { get; set; }
        public string values { get; set; }
    }

    public class FundAdminDashboardCommentryData
    {
        public List<CommentryHeaderModel> CommentryHeaderList { get; set; }
        public List<Dictionary<string, string>> CommentryDataList { get; set; }
        public DateTime LatestCommentryDate { get; set; }
        public string LatestCommentryValue { get; set; }
    }

    public class CommentryHeaderModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

}

