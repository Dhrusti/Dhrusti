using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class AddAppoitmentResDTO
    {
        public int Id { get; set; }
        public decimal? AccountNo { get; set; }
        public string PatientName { get; set; }
        public string? Status { get; set; }
    }
}
