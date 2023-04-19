namespace DTO.ReqDTO
{
    public class AddRoleReqDTO
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}
