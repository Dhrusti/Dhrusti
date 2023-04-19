namespace DTO.ReqDTO
{
    public class DeleteFundReqDTO
    {
        public int FundId { get; set; }
        public int UserId { get; set; }
        public string Pwd { get; set; } = null!;
    }
}
