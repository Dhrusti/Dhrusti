namespace DTO.ResDTO
{
    public class GetAllCityCustomResDTO
    {
        public int stateId { get; set; }
        public string StateName { get; set; }
        public List<CityList> cityList { get; set; }

    }
    public class CityList
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }


}
