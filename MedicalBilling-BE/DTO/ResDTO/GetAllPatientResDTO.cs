using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllPatientResDTO
    {
        public decimal? AccountNo { get; set; }

        public string PatientName { get; set; }
        public string PrimaryInsuranceName { get; set; }
        public string SecondaryInsuranceName { get; set; }
        public bool? Status { get; set; }
        public DateTime ApptTime { get; set; }
        public DateTime EntryTime { get; set; }
        public string DoName { get; set; }
        public string Notes { get; set; }
        public string Email{ get; set; } 


    }
}
