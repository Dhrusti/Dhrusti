namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddPricingReqViewModel
    {
        public int FundId { get; set; }
        public string TransactionDate { get; set; }
        public List<Dictionary<string, decimal>> DynamicPricingInputs { get; set; }
        public int CreatedBy { get; set; }
    }
}
