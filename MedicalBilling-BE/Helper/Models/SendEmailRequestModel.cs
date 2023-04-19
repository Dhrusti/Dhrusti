using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class SendEmailRequestModel
    {
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string? Attachment { get; set; }

    }
}
