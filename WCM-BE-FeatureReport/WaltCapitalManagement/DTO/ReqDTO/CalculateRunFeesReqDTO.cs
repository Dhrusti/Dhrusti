using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class CalculateRunFeesReqDTO
    {
        public int FundId { get; set; }
        public int FeesId { get; set; }
        public DateTime NextRunDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
