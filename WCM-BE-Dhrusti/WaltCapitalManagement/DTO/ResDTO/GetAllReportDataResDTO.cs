using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllReportDataResDTO
    {
        public List<AllReportDetails> AllReportDetailList { get; set; }
        public int TotalCount { get; set; }
    }

    public class AllReportDetails
    {
        public string ClientName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AccountNo { get; set; }
        public int WaltCapConsultant { get; set; }
        public string Currency { get; set; }
        public string PortfolioValue { get; set; }
        public string PortfolioManager { get; set; }
        public string InvestmentValue { get; set; }
        public string AccountValue { get; set; }
    }
}



