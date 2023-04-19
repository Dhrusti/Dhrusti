namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetFundForCTByFundIdResViewModel
    {
        public string FundName { get; set; }
        public string Currency { get; set; }
        public double UnitStartingPrice { get; set; }
        public List<string> UnitType { get; set; }
        public List<string> allocatedLists { get; set; }
    }

}
