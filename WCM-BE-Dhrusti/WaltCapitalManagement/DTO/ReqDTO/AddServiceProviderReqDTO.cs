namespace DTO.ReqDTO
{
    public class AddServiceProviderReqDTO
    {
        public int Id { get; set; }
        public string ServiceProvider { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
