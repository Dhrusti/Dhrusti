namespace Authentication_MicroService.ViewModels.ReqViewModels
{
	public class RegistrationReqViewModel
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string MobileNo { get; set; } = null!;

		public int Designation { get; set; }

		public string Address { get; set; } = null!;
	}
}
