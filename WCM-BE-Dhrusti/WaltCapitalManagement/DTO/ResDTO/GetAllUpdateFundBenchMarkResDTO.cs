using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllUpdateFundBenchMarkResDTO
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool? IsInDashboard { get; set; }
        public bool? IsRemoveMode { get; set; }
    }
}
