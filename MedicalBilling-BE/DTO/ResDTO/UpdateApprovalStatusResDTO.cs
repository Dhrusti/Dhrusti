using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class UpdateApprovalStatusResDTO
    {
        public int SenderId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        
    }
}
