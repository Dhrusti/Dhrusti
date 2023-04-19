namespace DTO.ResDTO
{
    public class GetStateByStateIdResDTO
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
    }
}
