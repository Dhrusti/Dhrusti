namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetPPMClientListResViewModel
    {
        public List<PPMClientDetail> PPMClientDetails { get; set; }
        public int TotalCount { get; set; }

    }
    public class PPMClientDetail
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public decimal AccValue { get; set; }
        public string Currency { get; set; }
    }
}

