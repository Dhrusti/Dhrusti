namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetPPMClientListTFSAResViewModel
    {
        public List<PPMClientTFSADetail> PPMClientTFSADetails { get; set; }
        public int TotalCount { get; set; }

    }
    public class PPMClientTFSADetail
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public decimal AccValue { get; set; }
        public string Currency { get; set; }
    }
}

