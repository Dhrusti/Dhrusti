namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetClientStatementByFundIdResViewModel
    {
        public List<ClientStatementDetails> ClientList { get; set; }
    }
    public class ClientStatementDetails
    {
        public string ClientAccountNo { get; set; }
        public string Name { get; set; }
       
    }
}

