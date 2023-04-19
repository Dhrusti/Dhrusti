namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class UpdateApprovalStatusReqViewModel
    {
        public int AdminId { get; set; }
        public int PatientId { get; set; }
        public int ReceptionistId { get; set; }
        public string? Status { get; set; }
    }
}
