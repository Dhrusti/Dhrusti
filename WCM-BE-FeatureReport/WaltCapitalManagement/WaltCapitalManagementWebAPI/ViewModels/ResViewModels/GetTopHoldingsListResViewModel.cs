namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetTopHoldingsListResViewModel
    {
        public List<HeaderValueModelList> HeaderValuemodel { get; set; }
        public List<Dictionary<string, string>> TableDatamodelList { get; set; }

    }
    public class HeaderValueModelList
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
