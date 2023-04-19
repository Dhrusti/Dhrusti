using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetIfaFeeReportResDTO
    {
        public List<FeeSummary> FeeSummaries { get; set; }
        public string Currency { get; set; }
        public string TotalFees { get; set; }

    }

    public class FeeSummary
    {
        public string FeeType { get; set; }
        public string Currency { get; set; }
        public string FeeAmount { get; set; }
    }
}
