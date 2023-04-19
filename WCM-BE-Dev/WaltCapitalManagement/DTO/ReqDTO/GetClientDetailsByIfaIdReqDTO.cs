using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetClientDetailsByIfaIdReqDTO
    {
        public int IfaId { get; set; }
        public int FundId { get; set; }
    }
}
