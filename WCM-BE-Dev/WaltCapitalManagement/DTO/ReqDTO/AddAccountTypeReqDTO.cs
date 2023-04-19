namespace DTO.ReqDTO
{
    public class AddAccountTypeReqDTO
    {
        public int UserId { get; set; }
        public string AccountType { get; set; } = null!;
    }
}
