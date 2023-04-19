namespace DTO.ResDTO
{
    public class GetRoleByIdResDTO
    {
        public List<RoleListById> roleListbyid { get; set; }
    }
    public class RoleListById
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleStatus { get; set; }
        public bool? IsRoleAssigned { get; set; }

    }
}
