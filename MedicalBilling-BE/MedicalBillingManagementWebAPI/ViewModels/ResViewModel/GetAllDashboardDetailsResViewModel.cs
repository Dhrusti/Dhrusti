namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllDashboardDetailsResViewModel
    {
        public List<GetAllClientResViewModel> getAllClientResViewModels  { get; set; }
        public List<GetAllDoctorResViewModel> getAllDoctorResViewModels  { get; set; }
        public List<GetAllExtensionResViewModel> getAllExtensionResViewModels  { get; set; }
        public List<GetAllCallTypeResViewModel> getAllCallTypeResViewModels  { get; set; }
        public List<GetAllApptDoctorResViewModel> getAllApptDoctorResViewModels  { get; set; }

        public decimal? AccountNo { get; set; }
    }
}
