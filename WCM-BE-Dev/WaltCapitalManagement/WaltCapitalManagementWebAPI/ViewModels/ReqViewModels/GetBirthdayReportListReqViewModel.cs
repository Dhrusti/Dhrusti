namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetBirthdayReportListReqViewModel
    {
        public int BirthdayBy { get; set; } // 0 : Today, 1 : Upcoming 
         public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
        //public string Alphabet { get; set; }

        public string SearchString { get; set; }
    }
}
