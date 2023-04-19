namespace DTO.ReqDTO
{
    public class CheckResetPasswordLinkReqDTO
    {
        public string Id { get; set; }
        public string Link { get; set; }
        public string SecurityCode { get; set; }
    }
}
