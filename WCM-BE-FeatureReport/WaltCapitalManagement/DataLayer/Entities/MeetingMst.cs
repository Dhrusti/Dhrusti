using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class MeetingMst
    {
        public int Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Venue { get; set; } = null!;
        public string Attendees { get; set; } = null!;
        public string ClientAction { get; set; } = null!;
        public string WaltCapitalActions { get; set; } = null!;
        public string Discussion { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
