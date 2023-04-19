using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetAllEmailbyIdReqDTO
    {
        public int UserId { get; set; }
        public string AppointmentType { get; set; }
    }
}
