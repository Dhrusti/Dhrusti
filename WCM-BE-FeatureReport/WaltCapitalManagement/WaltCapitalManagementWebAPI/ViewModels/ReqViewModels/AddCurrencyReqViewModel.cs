namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddCurrencyReqViewModel
    {
        public string CurrencyName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string BaseValue { get; set; } = null!;
        public int UserId { get; set; }
    }
}
