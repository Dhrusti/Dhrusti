using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ClientCsvdataMst
    {
        public int Id { get; set; }
        public string? AdviserCode { get; set; }
        public string? ClientName { get; set; }
        public string? RegistrationNumber { get; set; }
        public int? ClientNumber { get; set; }
        public string? Product { get; set; }
        public string? AccountName { get; set; }
        public string? AccountGroups { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime? InceptionDate { get; set; }
        public string? FundManager { get; set; }
        public string? FundName { get; set; }
        public string? FundCode { get; set; }
        public string? InitialAdvisorFee { get; set; }
        public string? AnnualAdvisorFee { get; set; }
        public DateTime? Section14AdvisorFeeRenewal { get; set; }
        public string? MonthlyDebitOrder { get; set; }
        public string? AnnuityIncomeRegularWithdrawal { get; set; }
        public string? AnnuityIncomeRegularWithdrawalFrequency { get; set; }
        public DateTime? AnnuityIncomeAnniversary { get; set; }
        public string? AccountFundAllocation { get; set; }
        public double? Units { get; set; }
        public double? UnitPriceCents { get; set; }
        public DateTime? PriceDate { get; set; }
        public string? FundCurrency { get; set; }
        public double? MarketValueInFundCurrency { get; set; }
        public double? ExchangeRate { get; set; }
        public double? MarketValueInRands { get; set; }
        public string? AnnuitRevisionEffectiveMonth { get; set; }
        public string? NetCapitalGainOrLoss { get; set; }
        public string? ModelPortFolioName { get; set; }
        public double? DimFee { get; set; }
        public double? RicFee { get; set; }
        public string? GroupRaemployer { get; set; }
        public int? ClientCsvfieldId { get; set; }
    }
}
