using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllCSVDataDocumentReqDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SearchString { get; set; }
    }
}
