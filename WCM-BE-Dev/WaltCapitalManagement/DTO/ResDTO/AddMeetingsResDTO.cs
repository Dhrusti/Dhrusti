using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class AddMeetingsResDTO
    {
        public int Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Venue { get; set; } = null!;
    }
}
