using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class SendMailResDTO
    {
        public string MailTo { get; set; } 
        public string EmailFor { get; set; } = null!;
        public string body { get; set; } = null!;
        public string PatientEmail { get; set; }
    }
}
