using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class UpdateCarResDTO
    {
        public string Model { get; set; } = null!;
        public int RegistrationId { get; set; }
        public int Price { get; set; }
        public int? Brand { get; set; }
        public DateTime BuyTime { get; set; }
    }
}
