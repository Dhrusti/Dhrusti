namespace DTO.ReqDTO
{
    public class AddCustomCityReqDTO
    {
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int CreatedBy { get; set; }
    }
}
