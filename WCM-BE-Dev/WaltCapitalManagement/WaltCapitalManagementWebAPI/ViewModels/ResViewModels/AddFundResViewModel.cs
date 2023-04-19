namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class AddFundResViewModel
    {
        public int FundId { get; set; }
        public string FundName { get; set; }
        public bool? IsFactSheetCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
