namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class AddRemarkReqViewModel
    {
        public decimal AppointmentNumber { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public int LoginUserId { get; set; }

        public int ReceiverId { get; set; }
    }
}
