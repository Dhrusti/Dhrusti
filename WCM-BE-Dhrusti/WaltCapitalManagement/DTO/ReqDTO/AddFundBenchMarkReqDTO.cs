using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddFundBenchMarkReqDTO
    {
        public string BenchMarkName { get; set; }
        public DateTime BenchMarkDate { get; set; }
        public decimal BenchMarkValue { get; set; }
        public int FundId { get; set; }
    }
}
