using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetTransactionUnitTypeByClientIdReqDTO
    {
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public string UnitType { get; set; }
    }
}
