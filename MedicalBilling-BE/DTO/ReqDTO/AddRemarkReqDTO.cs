using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddRemarkReqDTO
    {
        public decimal AppointmentNumber { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }

        public int LoginUserId { get; set; }

        public int ReceiverId { get; set; }

    }
}
