using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class UpdateApprovalStatusReqDTO
    {
        public int AdminId { get; set; }
        public int PatientId { get; set; }
        public int ReceptionistId { get; set; }
        public string? Status { get; set; }
    }
}
