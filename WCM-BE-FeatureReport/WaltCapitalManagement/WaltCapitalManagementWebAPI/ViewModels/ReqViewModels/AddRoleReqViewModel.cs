namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddRoleReqViewModel
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}
