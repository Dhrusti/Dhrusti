namespace Car_IndustryWebAPI.ViewModels.ResViewModels
{
    public class GetCarsResViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public int RegistrationId { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; } = null!;
    }
}
