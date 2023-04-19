namespace DTO.ReqDTO
{
    public class GetPortfolioClientDataReqDTO
    {
        public int ServiceProviderId { get; set; }
        public int ServiceProviderTypeId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool? Orderby { get; set; }
        public string? Alphabet { get; set; }
        public string? SearchClientString { get; set; }
        public string? SearchStockString { get; set; }
    }
}
