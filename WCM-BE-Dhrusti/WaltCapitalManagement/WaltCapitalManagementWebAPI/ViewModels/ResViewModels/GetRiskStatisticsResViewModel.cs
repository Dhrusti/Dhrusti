namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetRiskStatisticsResViewModel
    {
        public List<HeaderValueModel> HeaderValueList { get; set; }
        public List<Dictionary<string, string>> TableDataList { get; set; }

    }
    public class HeaderValueModel
    {
        public string Label { get; set; }
        public string Value { get; set; }

    }
}
