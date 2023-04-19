namespace DTO.ResDTO
{
    public class LogInResDTO
    {
        public dynamic UserDetail { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Disclaimer { get; set; }
        public dynamic AccessControl { get; set; }
        public dynamic Functionality { get; set; }
    }

    public class UserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccessCategory { get; set; }
        public string Role { get; set; }
        public int SoftwareAccessGroupId { get; set; }
        public bool? IsDeviceApproved { get; set; }
        public string DeviceId { get; set; }
        public bool IsActive { get; set; }


    }
}
