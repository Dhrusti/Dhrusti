namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetBirthdayReportResViewModel
    {
        public List<TodaysBirthday> TodaysBirthdayList { get; set; }
        public List<UpcommingBirthday> UpcommingBirthdayList { get; set; }
        public List<TaskMeeting> TaskMeetingList { get; set; }
        public List<DueDiligence> dueDiligencesLists { get; set; }
        public List<OverDueDiligence> overDueDiligencesLists { get; set; }
        public List<AML> aMLLists { get; set; }
    }
    public class TodaysBirthday
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class UpcommingBirthday
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class TaskMeeting
    {
        public string MeetingType { get; set; }
        public string MeetingWith { get; set; }
        public string MeetingTime { get; set; }
        public DateTime MeetingDate { get; set; }
    }

    public class DueDiligence
    {
        public string Name { get; set; }
    }

    public class OverDueDiligence
    {
        public string Name { get; set; }
    }
    public class AML
    {
        public string Name { get; set; }
    }
}
