using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddMeetingsReqDTO
    {
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Venue { get; set; } = null!;
        public string Attendees { get; set; } = null!;
        public string ClientAction { get; set; } = null!;
        public string WaltCapitalActions { get; set; } = null!;
        public string Discussion { get; set; } = null!;
    }
}
