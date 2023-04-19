using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddDurationReqDTO
    {
        public decimal AppointmentId { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
    }
}
