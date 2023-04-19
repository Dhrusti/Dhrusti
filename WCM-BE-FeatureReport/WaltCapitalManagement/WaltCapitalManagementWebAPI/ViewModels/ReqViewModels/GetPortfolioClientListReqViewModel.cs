﻿namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class GetPortfolioClientListReqViewModel
    {
        public int PorfolioUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool? Orderby { get; set; }
        public string? Alphabet { get; set; }
        public string? SearchString { get; set; }
    }
}
