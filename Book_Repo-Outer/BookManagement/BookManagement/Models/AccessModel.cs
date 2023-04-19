namespace BookManagement.Models
{
	public class AccessModel
	{
		public int AccessId { get; set; }
		public string AccessName { get; set; } = null!;
		public bool? IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
