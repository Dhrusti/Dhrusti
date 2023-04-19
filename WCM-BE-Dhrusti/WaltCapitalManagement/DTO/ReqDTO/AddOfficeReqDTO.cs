namespace DTO.ReqDTO
{
    public class AddOfficeReqDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Office { get; set; } = null!;
        public int UserId { get; set; }
    }
}
