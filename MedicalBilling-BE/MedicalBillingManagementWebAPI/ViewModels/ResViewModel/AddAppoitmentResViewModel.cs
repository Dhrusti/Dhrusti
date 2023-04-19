namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class AddAppoitmentResViewModel
    {
        public int Id { get; set; }
        public decimal? AccountNo { get; set; }
        public string PatientName { get; set; }

        public string? Status { get; set; }
    }
}
