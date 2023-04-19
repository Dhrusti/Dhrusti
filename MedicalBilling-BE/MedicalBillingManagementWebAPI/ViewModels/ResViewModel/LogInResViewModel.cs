namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class LogInResViewModel
    {
        public dynamic UserDetail { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
    public class UserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }

        public string Role { get; set; }
    }

}
