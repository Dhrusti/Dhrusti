namespace DTO.ResDTO
{
    public class GetFundResDTO
    {
        public int FundId { get; set; }
        public string FundName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsFactSheetCreated { get; set; }
    }
}
