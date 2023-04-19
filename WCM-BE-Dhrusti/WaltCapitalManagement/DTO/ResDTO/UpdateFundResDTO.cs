namespace DTO.ResDTO
{
    public class UpdateFundResDTO
    {
        public int FundId { get; set; }
        public string FundName { get; set; }
        public bool? IsFactSheetCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
