namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllAppointmentResViewModel
    {
        public int TotalCount { get; set; }
        public int NewSchedulingCount { get; set; }
        public int ReschedulingCount { get; set; }
        public int OtherCount { get; set; }
        public int DoneCount { get; set; }
        public string AppointmentType { get; set; }
        public List<AppointmentList> appointmentList { get; set; }

        public class AppointmentList
        {
            public int SR { get; set; }
            public decimal? AccountNo { get; set; }

            public string PatientName { get; set; }
            public string PrimaryInsuranceName { get; set; }
            public string SecondaryInsuranceName { get; set; }
            public string Status { get; set; }
            public string? ApptTime { get; set; }
            public string EntryTime { get; set; }
            public string DoName { get; set; }
            public string Notes { get; set; }
            public bool Email { get; set; }
            public bool Remark { get; set; }
            public DateTime? DOB { get; set; }
            public string CallType { get; set; }
            public int UserId { get; set; }
            public bool IsEditable { get; set; }
            public string PatientEmail { get; set; }
            public int ReceptionistId { get; set; }
            public int AdminId { get; set; }
        }
    }
}
