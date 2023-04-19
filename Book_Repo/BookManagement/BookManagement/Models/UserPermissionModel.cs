namespace BookManagement.Models
{
	public class UserPermissionModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Permissionid { get; set; }
        public int AccessId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

   
}
