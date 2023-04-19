using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetDashBoardOfficeResDTO
    {
        public List<Office> offices { get; set; }
    }
    public class Office { 
    
        public string office { get; set; }
    }
}
