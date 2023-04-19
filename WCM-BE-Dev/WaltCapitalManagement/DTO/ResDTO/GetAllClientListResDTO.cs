namespace DTO.ResDTO
{
    public class GetAllClientListResDTO
    {
        public int UserId { get; set; }
        public string ClientAccNo { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
