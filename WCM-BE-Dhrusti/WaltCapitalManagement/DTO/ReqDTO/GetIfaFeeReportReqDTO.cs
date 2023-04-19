using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetIfaFeeReportReqDTO
    {
        public int FundId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? FilterBy { get; set; }
    }
}
