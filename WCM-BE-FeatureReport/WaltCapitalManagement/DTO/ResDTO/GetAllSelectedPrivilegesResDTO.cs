namespace DTO.ResDTO
{
    public class GetAllSelectedPrivilegesResDTO
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; }
        public bool IsSelected { get; set; } = true;
        public int ParentId { get; set; } = 0;
        public int Layer { get; set; }
        public List<GetAllSelectedPrivilegesResDTO> AllPrivileges { get; set; }
    }
}
