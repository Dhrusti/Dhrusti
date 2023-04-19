namespace Helper.Models
{
    public class UserTockenDataModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TockenExpiredOn { get; set; }
        public DateTime RefreshTockenExpiredOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
