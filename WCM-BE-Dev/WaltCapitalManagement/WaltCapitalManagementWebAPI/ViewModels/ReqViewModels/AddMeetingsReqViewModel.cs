namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddMeetingsReqViewModel
    {
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Venue { get; set; } = null!;
        public string Attendees { get; set; } = null!;
        public string ClientAction { get; set; } = null!;
        public string WaltCapitalActions { get; set; } = null!;
        public string Discussion { get; set; } = null!;
    }
}
