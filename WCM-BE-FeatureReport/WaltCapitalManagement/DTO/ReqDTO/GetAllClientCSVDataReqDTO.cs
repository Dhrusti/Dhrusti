namespace DTO.ReqDTO
{
    public class GetAllClientCSVDataReqDTO
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
    }
}
