namespace DTO.ResDTO
{
    public class AddPrivilegeResDTO
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int AccessableCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
