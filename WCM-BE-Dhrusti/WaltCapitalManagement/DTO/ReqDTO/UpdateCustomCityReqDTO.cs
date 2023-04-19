namespace DTO.ReqDTO
{
    public class UpdateCustomCityReqDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
