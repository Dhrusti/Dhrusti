namespace AuthByCookiesPOC.Models.ReqModels
{
    public class LoginReq
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
