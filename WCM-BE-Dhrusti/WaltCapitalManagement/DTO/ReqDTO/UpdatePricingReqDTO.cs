using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class UpdatePricingReqDTO
    {
        public int FundId { get; set; }
        public string TransactionDate { get; set; }
        public List<Dictionary<string, decimal>> DynamicPricingInputs { get; set; }
        public int UpdatedBy { get; set; }
    }
}
