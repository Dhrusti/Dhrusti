namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllSelectedPrivilegesResViewModel
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; }
        public bool IsSelected { get; set; } = false;
        public int ParentId { get; set; }
        public int Layer { get; set; }
        public List<GetAllSelectedPrivilegesResViewModel> AllPrivileges { get; set; }
    }
}
