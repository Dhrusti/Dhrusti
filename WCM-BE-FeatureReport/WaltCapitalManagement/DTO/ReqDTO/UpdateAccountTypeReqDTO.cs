namespace DTO.ReqDTO
{
    public class UpdateAccountTypeReqDTO
    {
        public int Id { get; set; }
        public string AccountType { get; set; } = null!;
        public int UserId { get; set; }
    }
}
