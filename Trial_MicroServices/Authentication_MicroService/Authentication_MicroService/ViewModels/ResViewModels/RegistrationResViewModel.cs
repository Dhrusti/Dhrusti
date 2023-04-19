namespace Authentication_MicroService.ViewModels.ResViewModels
{
	public class RegistrationResViewModel
	{
		public int RegistrationId { get; set; }

		public string FullName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string MobileNo { get; set; } = null!;

		public int Designation { get; set; }

		public string Address { get; set; } = null!;
	}
}
