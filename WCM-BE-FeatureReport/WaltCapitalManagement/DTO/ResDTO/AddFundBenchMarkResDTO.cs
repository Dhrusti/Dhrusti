using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class AddFundBenchMarkResDTO
    {
        public int Id { get; set; }
        public string BenchMarkName { get; set; }
        public DateTime BenchMarkDate { get; set; }
        public decimal BenchMarkValue { get; set; }
    }
}
