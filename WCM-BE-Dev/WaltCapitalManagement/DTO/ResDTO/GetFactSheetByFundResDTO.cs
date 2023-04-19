namespace DTO.ResDTO
{
    public class GetFactSheetByFundResDTO
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string InvestmentObjective { get; set; } = null!;
        public string PortfolioManager { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Fsp { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public DateTime InceptionDate { get; set; }
        public string Sector { get; set; } = null!;
        public string Target { get; set; } = null!;
        public string ParticipatoryStructure { get; set; } = null!;
        public double MinInvestment { get; set; }
        public string AnnualFeesUnitA { get; set; } = null!;
        public string AnnualFeesUnitB { get; set; } = null!;
        public string BaseFee { get; set; } = null!;
        public string FeeHurdle { get; set; } = null!;
        public string PerformanceFeesUnitA { get; set; } = null!;
        public string PerformanceFeesUnitB { get; set; } = null!;
        public string FeeExample { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string? Recommended { get; set; }
        public string? ShortCommentary { get; set; }
        public string? Disclaimer { get; set; }
        public int FundRiskRating { get; set; }
        public string Currency { get; set; }
        public DateTime CurrentDate { get; set; }
    }


}
