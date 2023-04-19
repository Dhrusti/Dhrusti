namespace ERP_CRM.ViewModels.ResViewModel
{
	public class LoginResViewModel
	{
		public int TokenId { get; set; }

		//public int UserId { get; set; }

		public string Token { get; set; } = null!;

		public string RefreshToken { get; set; } = null!;

		public DateTime TokenCreated { get; set; }

		public DateTime TokenExpires { get; set; }

	}
}
