namespace DTO.ResDTO
{
    public class ModelPortfolioResDTO
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
