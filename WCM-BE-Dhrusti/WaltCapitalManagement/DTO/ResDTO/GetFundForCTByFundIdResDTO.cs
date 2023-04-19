namespace DTO.ResDTO
{
    public class GetFundForCTByFundIdResDTO
    {
        public string FundName { get; set; }
        public string Currency { get; set; }
        public List<string> UnitType { get; set; }
        public double UnitStartingPrice { get; set; }
        public List<string> allocatedLists { get; set; }
    }

}
