namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class AddMeetingsResViewModel
    {
        public int Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Venue { get; set; } = null!;
    }
}
