namespace DTO.ReqDTO
{
    public class UpdateRoleReqDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleStatus { get; set; }
        public int UpdatedBy { get; set; }
    }
}
