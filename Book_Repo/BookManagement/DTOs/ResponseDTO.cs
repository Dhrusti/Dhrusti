using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public dynamic Data { get; set; } = null;

    }
}
