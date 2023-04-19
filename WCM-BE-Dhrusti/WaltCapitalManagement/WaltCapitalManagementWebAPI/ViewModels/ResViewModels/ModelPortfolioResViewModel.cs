namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class ModelPortfolioResViewModel
    {
        public List<HeaderModel> HeaderList { get; set; }
        public List<Dictionary<string, string>> TableDataList { get; set; }

    }
    public class HeaderModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

}

