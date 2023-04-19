using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class UnitTypePercentageModel
    {
        public List<UnitTypePercentageDetail> UnitTypePercentageList { get; set; }
         
    }
    public class UnitTypePercentageDetail
    {
        public string UnitType { get; set; }
        public decimal UnitTypeValue { get; set; }
        public decimal UnitTypePercentage { get; set; }
    }
}
