using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FundDynamicInputPriceMst
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string UnitType { get; set; } = null!;
        public string Label { get; set; } = null!;
        public decimal Value { get; set; }
        public DateTime BalanceDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsAddedFromPricing { get; set; }
    }
}
