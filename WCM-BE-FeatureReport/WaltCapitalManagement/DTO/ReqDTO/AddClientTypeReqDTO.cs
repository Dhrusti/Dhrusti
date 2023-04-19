namespace DTO.ReqDTO
{
    public class AddClientTypeReqDTO
    {
        public int Id { get; set; }
        public string ClientType { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
