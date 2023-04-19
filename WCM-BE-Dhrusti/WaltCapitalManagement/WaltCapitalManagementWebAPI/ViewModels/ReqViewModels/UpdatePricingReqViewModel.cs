namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdatePricingReqViewModel
    {
        public int FundId { get; set; }
        public string TransactionDate { get; set; }
        public List<Dictionary<string, decimal>> DynamicPricingInputs { get; set; }
        public int UpdatedBy { get; set; }
    }
}
