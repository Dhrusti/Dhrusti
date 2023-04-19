namespace DTO.ReqDTO
{
    public class GetPricingReqDTO
    {
        public int FundId { get; set; }
        public string? TransactionDate { get; set; }
        public string FeeUnit { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool? Orderby { get; set; }
    }
}
