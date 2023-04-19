namespace DTO.ReqDTO
{
    public class GetAllCSVDataReqDTO
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
    }
}
