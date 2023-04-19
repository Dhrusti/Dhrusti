using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetRunFeesResDTO
    {
        public List<RunFeesResDetail> RunFeesList { get; set; }
        public string TotalRunFeesStr { get; set; }
    }
    public class RunFeesResDetail
    {
        public int FeesId { get; set; }
        public string FeesName { get; set; }
        public DateTime LastRunDate { get; set; }
        public string LastAmountStr { get; set; }
        public DateTime NextRunDate { get; set; }
        public string PendingAmountStr { get; set; }
        public string TotalStr { get; set; }
        public string VATStr { get; set; }
        public string TotalInclVATStr { get; set; }
    }
}
