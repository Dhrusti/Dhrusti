namespace DTO.ResDTO
{
    public class GetFactSheetByIdResDTO
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
        public string MinInvestment { get; set; } = null!;
        public string? PerformanceFee { get; set; }
        public string BaseFee { get; set; } = null!;
        public string FeeHurdle { get; set; } = null!;
        public string SharingRatio { get; set; } = null!;
        public string FeeExample { get; set; } = null!;
        public string? Recommended { get; set; }
        public string? ShortCommentary { get; set; }
        public string? Disclaimer { get; set; }
        public DateTime CurrentDate { get; set; }

    }
}
