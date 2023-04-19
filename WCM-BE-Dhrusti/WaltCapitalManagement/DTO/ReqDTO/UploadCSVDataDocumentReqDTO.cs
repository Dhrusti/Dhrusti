using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class UploadCSVDataDocumentReqDTO
    {
        public int? UserId { get; set; }
        public int? ServiceProviderId { get; set; }
        public dynamic File { get; set; }
    }
}
