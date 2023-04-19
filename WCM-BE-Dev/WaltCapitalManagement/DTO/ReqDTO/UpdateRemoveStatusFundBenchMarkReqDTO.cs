using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class UpdateRemoveStatusFundBenchMarkReqDTO
    {
        public int Id { get; set; }
        public bool? IsRemoveMode { get; set; }
    }
}
