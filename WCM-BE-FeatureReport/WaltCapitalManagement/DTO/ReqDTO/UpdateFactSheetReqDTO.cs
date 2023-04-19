namespace DTO.ReqDTO
{
    public class UpdateFactSheetReqDTO
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
        public double AnnualFeesUnitA { get; set; }
        public double AnnualFeesUnitB { get; set; }
        public double BaseFee { get; set; }
        public string FeeHurdle { get; set; } = null!;
        public double PerformanceFeesUnitA { get; set; }
        public double PerformanceFeesUnitB { get; set; }
        public string FeeExample { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string? Recommended { get; set; }
        public string? ShortCommentary { get; set; }
        public string? Disclaimer { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
