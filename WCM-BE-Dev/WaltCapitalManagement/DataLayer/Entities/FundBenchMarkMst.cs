using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FundBenchMarkMst
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string BenchMarkName { get; set; } = null!;
        public DateTime BenchMarkDate { get; set; }
        public decimal BenchMarkValue { get; set; }
        public bool? IsAddMode { get; set; }
        public bool? IsInDashboard { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
