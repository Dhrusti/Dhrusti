namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetInteractiveBrokersClientListResViewModel
    {
        public List<InteractiveBrokersClientList> InteractiveBrokersClientLists { get; set; }
        public int TotalCount { get; set; }
    }

    public class InteractiveBrokersClientList
    {
        public string AccountNo { get; set; }
        public string WaltCapNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PortfolioValue { get; set; }
        public string PortfolioManager { get; set; }
    }
}
