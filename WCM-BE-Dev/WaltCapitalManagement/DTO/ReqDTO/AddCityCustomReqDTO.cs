namespace DTO.ReqDTO
{
    public class AddCityCustomReqDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int CreatedBy { get; set; }
    }
}
