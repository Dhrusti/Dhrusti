namespace DTO.ReqDTO
{
    public class UpdateClientTypeReqDTO
    {
        public int Id { get; set; }
        public string ClientType { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
