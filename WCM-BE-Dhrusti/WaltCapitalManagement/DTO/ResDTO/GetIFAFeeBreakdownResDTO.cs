namespace DTO.ResDTO
{
    public class GetIFAFeeBreakdownResDTO
    {
        public List<IfaFeeBreakdown> IfaFeeBreakdown { get; set; }
        public DateTime CurrentMonthEndDate { get; set; }
        public bool IsCurrencyZAR { get; set; }
        public int TotalCount { get; set; }
        public string TotalIFAAssets { get; set; }
    }

    public class IfaFeeBreakdown
    {
        public int IfaId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeleNo { get; set; }
        public string Email { get; set; }
        public string ClientAccNo { get; set; }
        public string AUM { get; set; }
        public string ZARFee { get; set; }
        public string VAT { get; set; }
        public string TotalZARFee { get; set; }
        public string USDFee { get; set; }
    }
}
