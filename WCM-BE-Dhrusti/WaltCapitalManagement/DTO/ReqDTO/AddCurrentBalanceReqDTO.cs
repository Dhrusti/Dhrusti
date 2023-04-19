namespace DTO.ReqDTO
{
    public class AddCurrentBalanceReqDTO
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal FundBalance { get; set; }
        public decimal UnitBalance { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }

    }
}
