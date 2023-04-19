namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllenGrayClientListResViewModel
    {
        public List<AllenGrayClientList> AllenGrayClientLists { get; set; }
        public int TotalCount { get; set; }
    }

    public class AllenGrayClientList
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAccNo { get; set; }
        public string InvestmentValue { get; set; }
    }
}
