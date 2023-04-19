namespace DTO.ReqDTO
{
    public class AccessCategoryReqDTO
    {
        public string AccessCategory { get; set; } = null!;
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public int CreatedBy { get; set; }
    }
}
