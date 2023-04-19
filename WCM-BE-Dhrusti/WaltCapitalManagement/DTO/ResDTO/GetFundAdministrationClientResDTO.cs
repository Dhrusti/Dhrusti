namespace DTO.ResDTO
{
    public class GetFundAdministrationClientResDTO
    {
        public string TotalUnitCount { get; set; }
        public string TotalValueCount { get; set; }

        public string PageTotalUnitCount { get; set; }
        public string PageTotalValueCount { get; set; }
        public int TotalCount { get; set; }
        public List<FundAdministrationClient> FundAdministrationClientList { get; set; }

    }

    public class FundAdministrationClient
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string AccountNo { get; set; }
        public string UnitsString { get; set; }
        public string ValueString { get; set; }
        public string CostNAV { get; set; }
        public string CurrentUnitPrice { get; set; }
        public string CurrentNAV { get; set; }
        public string OwnerShipStockPercentage { get; set; }
        public string OwnerShipFundPercentage { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public double Units { get; set; }
        public double Value { get; set; }

        public string? UnitType { get; set; }
    }

}
