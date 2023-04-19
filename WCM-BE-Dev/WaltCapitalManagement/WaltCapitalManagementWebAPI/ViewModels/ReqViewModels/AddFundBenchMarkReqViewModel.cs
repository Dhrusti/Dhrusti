namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddFundBenchMarkReqViewModel
    {
        public string BenchMarkName { get; set; }
        public DateTime BenchMarkDate { get; set; }
        public decimal BenchMarkValue { get; set; }
        public int FundId { get; set; }

    }
}
