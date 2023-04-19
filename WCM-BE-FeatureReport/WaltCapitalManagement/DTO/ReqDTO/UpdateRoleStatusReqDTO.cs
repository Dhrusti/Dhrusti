namespace DTO.ReqDTO
{
    public class UpdateRoleStatusReqDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
