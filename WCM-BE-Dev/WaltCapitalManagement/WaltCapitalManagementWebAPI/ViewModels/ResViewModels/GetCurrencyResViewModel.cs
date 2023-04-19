namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetCurrencyResViewModel
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string BaseValue { get; set; } = null!;
    }
}
