using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllClientResDTO
    {
        public string ClientName { get; set; }

        public string OfficeName { get; set; } = null!;

        public string Address { get; set; }
        public string MobileNo { get; set; } = null!;

        public string FaxNo { get; set; } = null!;

        public string InfoEmail { get; set; } = null!;

        public string AppoitmentEmail { get; set; } = null!;

        public string DoctorEmail { get; set; } = null!;
     

    }
}
