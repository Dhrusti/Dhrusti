namespace DTO.ReqDTO
{
    public class AddCurrencyReqDTO
    {
        public string CurrencyName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string BaseValue { get; set; } = null!;
        public int UserId { get; set; }
    }
}
