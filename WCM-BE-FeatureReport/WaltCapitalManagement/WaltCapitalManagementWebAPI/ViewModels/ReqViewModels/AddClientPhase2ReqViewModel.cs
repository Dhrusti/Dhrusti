namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class AddClientPhase2ReqViewModel
    {
        public int Id { get; set; }
        public bool Equity { get; set; }
        public bool Tfsa { get; set; }
        public bool WCFundAdministration { get; set; }
        public bool Dcs { get; set; }
        public bool Mcs { get; set; }
        public string? InitialFee { get; set; } = "0";
        public string? AnnualManagementFee { get; set; } = "0";
        public string? PerformanceFee { get; set; } = "0";
        public string? BrokerageRate { get; set; } = "0";
        public string? FlatBrokerageRate { get; set; } = "0";
        public string? AdminMonthlyFee { get; set; } = "0";
        public string? Other { get; set; } = "0";
        public bool IsVatapplicable { get; set; }
        public bool LoadWithoutFee { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
