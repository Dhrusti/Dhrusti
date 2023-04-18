using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CommonResponse
    {
        public bool Status { get; set; } = false;
        public int StatusCode { get; set; } = Convert.ToInt32(HttpStatusCode.InternalServerError);
        public string Message { get; set; } = "Exception";
        public dynamic Data { get; set; }
    }
}
