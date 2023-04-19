namespace DTO.ReqDTO
{
    public class AddServiceProviderTypeReqDTO
    {
        public int Id { get; set; }
        public string ServiceProviderType { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
