namespace DTO.ResDTO
{
    public class GetPortfolioClientListResDTO
    {
        public List<GetPortfolioClientList> getPortfolioClientLists { get; set; }
        public int TotalCount { get; set; }

    }
    public class GetPortfolioClientList
    {
        public int UserId { get; set; }
        public string ClientAccNo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string WorkNo { get; set; }
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Status { get; set; }
    }
}
