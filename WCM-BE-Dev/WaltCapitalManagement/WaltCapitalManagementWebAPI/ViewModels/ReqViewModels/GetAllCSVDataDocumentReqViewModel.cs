namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetAllCSVDataDocumentReqViewModel
    {

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SearchString { get; set; }
    }
}
