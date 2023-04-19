namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AccessCategoryReqViewModel
    {
        public string AccessCategory { get; set; } = null!;
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public int CreatedBy { get; set; }
    }
}
