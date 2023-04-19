namespace DTO.ReqDTO
{
    public class AddPricingReqDTO
    {
        public int FundId { get; set; }
        public string TransactionDate { get; set; }
        public List<Dictionary<string, decimal>> DynamicPricingInputs { get; set; }
        public int CreatedBy { get; set; }
    }
}
