namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetBirthdayReportListResViewModel
    {

        public List<BirthdayReport> BirthdayReports { get; set; }
        public int TotalCount { get; set; }

    }

    public class BirthdayReport
    {
        public int Id { get; set; }
        public string FirstChar { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
