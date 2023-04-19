namespace DTO.ResDTO
{
    public class GetMonthlyPerformanceResDTO
    {

        public List<Dictionary<string, string>> YearList { get; set; }

        public List<MonthlyPerformanceHead> MonthlyPerformanceHead { get; set; }
        public List<Dictionary<string, string>> MonthlyPerformanceTableData { get; set; }
    }
    public class MonthlyPerformanceHead
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }
    }

}
