namespace ValidationDemoApi.Entities
{
    public class CurrencyResModel
    {
        public DateOnly date { get; set; }
        public string result { get; set; }
        public string success { get; set; }
    }

    public class infoList
    {
        public DateTime timestamp { get; set; }
        public  double rate { get; set; }
    }
    public class QueryList {
        public double Amount { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public double rate { get; set; }
    }
}
