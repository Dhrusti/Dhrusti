using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class FundDynamicFieldHistoryMst
    {
        public int Id { get; set; }
        public int DynamicFieldId { get; set; }
        public int FundId { get; set; }
        public string Label { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int? RowId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
