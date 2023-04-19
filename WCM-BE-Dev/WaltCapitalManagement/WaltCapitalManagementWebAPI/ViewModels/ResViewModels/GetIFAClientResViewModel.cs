namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetIFAClientResViewModel
    {
        public int TotalCount { get; set; }
        public List<IFAClientList> ifaClientList { get; set; }
    }
    public class IFAClientList
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string ClientId { get; set; }
        public int IFAId { get; set; }
        public string ClientAccNo { get; set; }

    }

}

