namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class AddRemarkResViewModel
    {
        public decimal? AppointmentNumber { get; set; }
        public string Remarks { get; set; }
        public int ClickType { get; set; }
        public int RemarkId { get; set; }
    }
}
