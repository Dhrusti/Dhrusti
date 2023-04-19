namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class SendNotificationReqViewModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CreatedBy { get; set; }
    }
}
