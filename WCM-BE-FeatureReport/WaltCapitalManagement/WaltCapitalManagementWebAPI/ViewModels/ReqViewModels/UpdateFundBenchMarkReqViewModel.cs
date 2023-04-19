namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateFundBenchMarkReqViewModel
    {
        public int FundId { get; set; }
        public DateTime Date { get; set; }

        public List<BenchMarks> benchmarks { get; set; }

        public class BenchMarks
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public string Value { get; set; }
            public bool? IsInDashboard { get; set; }
            public bool? IsRemoveMode { get; set; }

        }
    }
}
