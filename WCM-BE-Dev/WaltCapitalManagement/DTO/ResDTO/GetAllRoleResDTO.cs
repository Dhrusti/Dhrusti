namespace DTO.ResDTO
{
    public class GetAllRoleResDTO
    {

        public int TotalCount { get; set; }
        public List<RoleList> roleList { get; set; }
    }
    public class RoleList
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleStatus { get; set; }
        public bool? IsRoleAssigned { get; set; }

    }
}

