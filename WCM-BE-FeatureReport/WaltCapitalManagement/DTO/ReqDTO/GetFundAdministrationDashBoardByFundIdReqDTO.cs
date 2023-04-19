using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetFundAdministrationDashBoardByFundIdReqDTO
    {
        public int FundId { get; set; }
        public int FundBenchmarkId { get; set; }
    }
}
