namespace DTO.ReqDTO
{
    public class UpdateServiceProviderTypeReqDTO
    {
        public int Id { get; set; }
        public string ServiceProviderType { get; set; } = null!;
        public int UpdateBy { get; set; }
    }
}
