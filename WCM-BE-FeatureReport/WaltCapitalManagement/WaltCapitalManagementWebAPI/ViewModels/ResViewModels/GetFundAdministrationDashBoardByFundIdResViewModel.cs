namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetFundAdministrationDashBoardByFundIdResViewModel
    {
        public List<GetFundAdminDarshboardGraphData> getFundAdminDarshboardGraphs { get; set; }
        public List<GetFundAdministrationCommentoryHeader> getFundAdministrationCommentoryHeaders { get; set; }
        public List<GetFundAdministrationCommentoryValue> getFundAdministrationCommentoryValues { get; set; }
        public List<GetFundAdministrationFundReturns1> getFundAdministrationFundReturns1 { get; set; }
        public List<GetFundAdministrationFundReturns2> getFundAdministrationFundReturns2 { get; set; }

    }

    public class GetFundAdminDarshboardGraphData
    {
        public string Title { get; set; }
        public string values { get; set; }
    }

    public class GetFundAdministrationCommentoryHeader
    {
        public string Title { get; set; }
        public string values { get; set; }
    }

    public class GetFundAdministrationCommentoryValue
    {
        public string Title { get; set; }
        public string values { get; set; }
    }
    public class GetFundAdministrationFundReturns1
    {
        public string Title { get; set; }
        public string values { get; set; }
    }
    public class GetFundAdministrationFundReturns2
    {
        public string Title { get; set; }
        public string values { get; set; }
    }
}

