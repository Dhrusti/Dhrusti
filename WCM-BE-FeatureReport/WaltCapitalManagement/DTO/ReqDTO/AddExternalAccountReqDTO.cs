namespace DTO.ReqDTO
{
    public class AddExternalAccountReqDTO

    {
        public int ServiceProvider { get; set; }
        public int Type { get; set; }
        public string AccountCode { get; set; } = null!;
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
    }
}
