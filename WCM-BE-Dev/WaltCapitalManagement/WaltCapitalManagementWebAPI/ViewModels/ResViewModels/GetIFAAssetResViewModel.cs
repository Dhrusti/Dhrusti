namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetIFAAssetResViewModel
    {
        public int TotalCount { get; set; }
        public int TotalIFACount { get; set; }
        public List<IFAAsseList> ifaAssetList { get; set; }
    }
    public class IFAAsseList
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AUM { get; set; } = 100!;
        public string ClientAccNo { get; set; }
        public string? Status { get; set; }

    }

}
