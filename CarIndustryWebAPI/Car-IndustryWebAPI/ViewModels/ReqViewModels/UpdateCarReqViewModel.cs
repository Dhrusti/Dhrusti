namespace Car_IndustryWebAPI.ViewModels.ReqViewModels
{
    public class UpdateCarReqViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public int RegistrationId { get; set; }
        public int Price { get; set; }
        public int? Brand { get; set; }
        public DateTime BuyTime { get; set; }
        public int UserId { get; set; }
    }
}
