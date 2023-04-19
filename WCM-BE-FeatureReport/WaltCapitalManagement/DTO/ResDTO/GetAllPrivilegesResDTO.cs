namespace DTO.ResDTO
{
    public class GetAllPrivilegesResDTO
    {
        public int Id { get; set; }
        public string AccessCategory { get; set; }
        public int ParentId { get; set; }
        public bool IsSelected { get; set; } = false;
        public int Layer { get; set; }
        public List<GetAllPrivilegesResDTO> AllPrivileges { get; set; }
    }
}
