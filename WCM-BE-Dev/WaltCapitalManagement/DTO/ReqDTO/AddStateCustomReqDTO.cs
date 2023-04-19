namespace DTO.ReqDTO
{
    public class AddStateCustomReqDTO
    {
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public int CreatedBy { get; set; }
    }
}
