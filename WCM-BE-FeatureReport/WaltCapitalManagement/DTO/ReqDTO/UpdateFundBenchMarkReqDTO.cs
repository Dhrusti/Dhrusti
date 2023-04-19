using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class UpdateFundBenchMarkReqDTO
    {
      
        public int FundId { get; set; }
        public DateTime Date { get; set; }

        public List<BenchMarks> benchmarks { get; set; }

        public class BenchMarks
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public string Value { get; set; } 
            public bool? IsInDashboard { get; set; }
            public bool? IsRemoveMode { get; set; }

        }

    }
}
