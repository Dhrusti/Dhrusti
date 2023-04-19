namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateRoleReqViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleStatus { get; set; }
        public int UpdatedBy { get; set; }
    }
}
