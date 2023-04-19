namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class LogInResViewModel
    {
        public dynamic UserDetail { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Disclaimer { get; set; }
        public dynamic AccessControl { get; set; }
        public dynamic Functionality { get; set; }
    }
}
