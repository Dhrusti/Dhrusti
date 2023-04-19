namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllUpdateFundBenchMarkResViewModel
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool? IsInDashboard { get; set; }
        public bool? IsRemoveMode { get; set; }
    }
}
