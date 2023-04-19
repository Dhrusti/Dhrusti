using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FactSheetMst
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
        public double AnnualFeesUnitC { get; set; }
        public double? BaseFee { get; set; }
        public string Recommended { get; set; } = null!;
        public string ShortCommentary { get; set; } = null!;
        public string Disclaimer { get; set; } = null!;
        public string FeeHurdle { get; set; } = null!;
        public double PerformanceFeesUnitA { get; set; }
        public double PerformanceFeesUnitB { get; set; }
        public double PerformanceFeesUnitC { get; set; }
        public string FeeExample { get; set; } = null!;
        public string Method { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
