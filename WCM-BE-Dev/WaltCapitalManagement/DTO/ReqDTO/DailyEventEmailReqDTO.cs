using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class DailyEventEmailReqDTO
    {
        public int SenderId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string TemplateName { get; set; }
        public string UploadTemplate { get; set; }  
        public string Message { get; set; } = null!;

        //public int CreatedBy { get; set; }
    }
}
