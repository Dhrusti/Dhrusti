namespace DTO.ReqDTO
{
    public class UpdatePrivilegesReqDTO
    {
        public int GroupId { get; set; }
        public int UpdatedBy { get; set; }
        public List<int> Privileges { get; set; }
    }
}
