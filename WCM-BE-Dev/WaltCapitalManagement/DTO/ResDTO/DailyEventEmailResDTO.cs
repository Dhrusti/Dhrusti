using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class DailyEventEmailResDTO
    {
        public string FullName { get; set; } = null!;
        public string EmailFor { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
