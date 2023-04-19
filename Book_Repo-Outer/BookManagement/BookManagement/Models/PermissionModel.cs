namespace BookManagement.Models
{
	public class PermissionModel
	{
		public int Pid { get; set; }
		public string PermissionName { get; set; } = null!;
		public bool? IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
