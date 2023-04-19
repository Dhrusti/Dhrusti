using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetPPMClientListReqDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
        public string? Alphabet { get; set; }
        public string? SearchString { get; set; }
    }
}
