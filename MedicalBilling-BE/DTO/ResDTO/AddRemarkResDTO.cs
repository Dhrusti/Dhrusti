using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class AddRemarkResDTO
    {
        public decimal? AppointmentNumber { get; set; }
        public string Remarks { get; set; }
        public int ClickType { get; set; }
        public int RemarkId { get; set; }
    }
}
