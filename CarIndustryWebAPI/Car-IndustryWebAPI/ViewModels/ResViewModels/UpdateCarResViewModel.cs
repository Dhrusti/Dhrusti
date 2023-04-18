namespace Car_IndustryWebAPI.ViewModels.ResViewModels
{
    public class UpdateCarResViewModel
    {
        public string Model { get; set; } = null!;
        public int RegistrationId { get; set; }
        public int Price { get; set; }
        public int? Brand { get; set; }
        public DateTime BuyTime { get; set; }
    }
}
