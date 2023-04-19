using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetClientStatementReportResDTO
    {
        public int  FundId { get; set; }
        public string FundName { get; set; }
        public string FundCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public string? ClientAccNo { get; set; }
        public int IFAId { get; set; }
        public string ClientName { get; set; }
        public string IFAName { get; set; }
        public string IFAConsultants { get; set; }
        public string ContactNo { get; set; }
        public decimal Deposit { get; set; }
        public decimal CapitalGrowthBonusGrowthSinceInception { get; set; }
        public decimal TransactionInProgress { get; set; }
        public decimal ClosingBalanceIncludingTransactionsInProgress { get; set; }
        public decimal ClosingBalancePricedInGold { get; set; }
    }
}
