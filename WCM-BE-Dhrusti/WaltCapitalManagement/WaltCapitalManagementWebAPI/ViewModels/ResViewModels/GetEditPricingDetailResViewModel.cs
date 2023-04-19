namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetEditPricingDetailResViewModel
    {
        public dynamic DynamicPricingInputValidation { get; set; }
        public List<string> DynamicPricingInputFields { get; set; }
    }
    public class RequiredDataModel
    {
        public dynamic Required { get; set; }
        public decimal InputValue { get; set; }
    }
}
