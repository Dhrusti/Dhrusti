namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetAllPrivilegesResViewModel
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; }
        public bool IsSelected { get; set; } = false;
        public int ParentId { get; set; }
        public int Layer { get; set; }
        public List<GetAllPrivilegesResViewModel> AllPrivileges { get; set; }
    }
}
