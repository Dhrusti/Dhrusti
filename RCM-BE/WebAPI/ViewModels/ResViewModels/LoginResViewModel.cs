namespace WebAPI.ViewModels.ResViewModels
{
    public class LoginResViewModel
    {
		//public dynamic UserDetail { get; set; }
		//public string Token { get; set; }
		//public string RefreshToken { get; set; }
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;
		public string UserName { get; set; } = null!;
	}
}
