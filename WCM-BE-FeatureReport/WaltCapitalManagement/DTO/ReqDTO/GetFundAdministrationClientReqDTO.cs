namespace DTO.ReqDTO
{
    public class GetFundAdministrationClientReqDTO
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
        public string Alphabet { get; set; }

        public string SearchString { get; set; }

        public int FundId { get; set; }
        public string? UnitType { get; set; }
    }
}
