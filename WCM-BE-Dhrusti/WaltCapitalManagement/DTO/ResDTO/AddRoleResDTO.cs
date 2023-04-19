namespace DTO.ResDTO
{
    public class AddRoleResDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; }
        public bool? RoleStatus { get; set; }
    }
}
