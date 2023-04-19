namespace DTO.ReqDTO
{
    public class UpdateServiceProviderReqDTO
    {
        public int Id { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
