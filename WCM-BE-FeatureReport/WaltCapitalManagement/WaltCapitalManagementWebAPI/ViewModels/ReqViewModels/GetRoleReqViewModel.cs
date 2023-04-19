namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetRoleReqViewModel
    {
        public bool Status { get; set; } = false;
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
    }
}
