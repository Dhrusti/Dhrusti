namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetPricingResViewModel
    {
        public List<TableHeaderModel> HeaderList { get; set; }
        public List<Dictionary<string, string>> TableDataList { get; set; }
    }

    public class TableHeaderModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class TableDataModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

}
