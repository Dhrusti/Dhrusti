namespace DTO.ReqDTO
{
    public class UploadClientCSVDataReqDTO
    {
        public string AdviserCode { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public double? RegistrationNumber { get; set; }
        public int ClientNumber { get; set; }
        public string Product { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string AccountGroups { get; set; } = null!;
        public int AccountNumber { get; set; }
        public DateTime InceptionDate { get; set; }
        public string FundManager { get; set; } = null!;
        public string FundName { get; set; } = null!;
        public string FundCode { get; set; } = null!;
        public double InitialAdvisorFee { get; set; }
        public double AnnualAdvisorFee { get; set; }
        public double Section14AdvisorFeeRenewal { get; set; }
        public double MonthlyDebitOrder { get; set; }
        public double AnnuityIncomeRegularWithdrawal { get; set; }
        public double AnnuityIncomeRegularWithdrawalFrequency { get; set; }
        public DateTime AnnuityIncomeAnniversary { get; set; }
        public double AccountFundAllocation { get; set; }
        public double Units { get; set; }
        public double UnitPriceCents { get; set; }
        public DateTime PriceDate { get; set; }
        public string? FundCurrency { get; set; }
        public double MarketValueInFundCurrency { get; set; }
        public double ExchangeRate { get; set; }
        public double MarketValueInRands { get; set; }
        public int AnnuitRevisionEffectiveMonth { get; set; }
        public double NetCapitalGainOrLoss { get; set; }
        public string ModelPortFolioName { get; set; } = null!;
        public double DimFee { get; set; }
        public double RicFee { get; set; }
        public string GroupRaemployer { get; set; } = null!;
    }
}
