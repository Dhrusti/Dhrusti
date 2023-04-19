namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetFundBenchMarkResViewModel
    {
        public int TotalCount { get; set; }
        public List<FundBenchMarkList> fundBenchMarkList { get; set; }
    }
    public class FundBenchMarkList
    {
        public int Id { get; set; }
        public string BenchMarkName { get; set; } = null!;
        public string BenchMarkDate { get; set; }
        public string BenchMarkValue { get; set; }
        public bool? IsInDashboard { get; set; }
        public bool? IsRemoveMode { get; set; }
    }
}

