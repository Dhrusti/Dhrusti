namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class AccessCategoryResViewModel
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; } = null!;
        public int ParentId { get; set; }
        public int TypeId { get; set; }
    }
}
