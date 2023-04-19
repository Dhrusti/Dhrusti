namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class SendEmailReqViewModel
    {
        public int LoginUserId { get; set; }
        public int UserId { get; set; }
        public int MailTo { get; set; }
        public string Subject { get; set; } = null!;
        public string SubjectText { get; set; } = null!;
        public string Body { get; set; }
        public string PatientEmail { get; set; }

    }
}
