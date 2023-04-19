namespace DTO.ReqDTO
{
    public class GetTranscationTypeByClientIdReqDTO
    {
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public string UnitType { get; set; }
    }
}
