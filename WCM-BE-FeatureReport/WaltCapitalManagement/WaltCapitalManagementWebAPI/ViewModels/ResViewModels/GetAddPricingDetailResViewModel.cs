namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAddPricingDetailResViewModel
    {
        public dynamic DynamicPricingInputValidation { get; set; }
        public List<string> DynamicPricingInputFields { get; set; }
    }

    public class RequiredData
    {
        public bool Value { get; set; } = true;
        public string Message { get; set; }
    }

    public class RequiredModel
    {
        public dynamic Required { get; set; }
    }
}
