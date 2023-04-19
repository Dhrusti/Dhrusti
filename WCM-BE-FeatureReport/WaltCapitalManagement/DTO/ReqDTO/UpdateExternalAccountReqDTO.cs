namespace DTO.ReqDTO
{
    public class UpdateExternalAccountReqDTO
    {
        public int Id { get; set; }
        public int ServiceProvider { get; set; }
        public int Type { get; set; }
        public string AccountCode { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
