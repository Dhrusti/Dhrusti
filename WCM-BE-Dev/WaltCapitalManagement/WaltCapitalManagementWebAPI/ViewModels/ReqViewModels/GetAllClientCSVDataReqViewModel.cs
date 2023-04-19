namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetAllClientCSVDataReqViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
    }
}
