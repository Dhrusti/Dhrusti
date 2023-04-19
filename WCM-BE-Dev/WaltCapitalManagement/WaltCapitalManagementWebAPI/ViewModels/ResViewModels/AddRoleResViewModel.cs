namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class AddRoleResViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; }
        public bool? RoleStatus { get; set; }
    }
}
