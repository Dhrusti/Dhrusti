namespace DTO.ResDTO
{
    public class GetPricingResDTO
    {
        public List<TableHeaderModel> HeaderList { get; set; }
        public List<Dictionary<string, string>> TableDataList { get; set; }
    }

    public class TableHeaderModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
