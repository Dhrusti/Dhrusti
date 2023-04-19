using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ClientTransactionMst
    {
        public int Id { get; set; }
        public int Fund { get; set; }
        public int Client { get; set; }
        public int Ifa { get; set; }
        public double IfaupFrontFee { get; set; }
        public double IfaAnnualFee { get; set; }
        public string TransactionType { get; set; } = null!;
        public string TransactionIn { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public double NumberOfUnits { get; set; }
        public double UnitPrice { get; set; }
        public string AllocateTo { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal? UnitBalance { get; set; }
        public decimal? AmountBalance { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string? UnitType { get; set; }
    }
}
