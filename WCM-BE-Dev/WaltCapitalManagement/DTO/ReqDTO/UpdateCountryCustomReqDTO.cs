namespace DTO.ReqDTO
{
    public class UpdateCountryCustomReqDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public int UpdatedBy { get; set; }
    }
}
