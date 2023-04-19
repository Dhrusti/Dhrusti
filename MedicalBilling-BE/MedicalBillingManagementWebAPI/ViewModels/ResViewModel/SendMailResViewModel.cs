namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class SendMailResViewModel
    {
        public string MailTo { get; set; }
        public string EmailFor { get; set; } = null!;
        public string body { get; set; } = null!;
    }
}
