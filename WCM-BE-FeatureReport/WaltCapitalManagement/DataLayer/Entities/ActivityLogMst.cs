using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ActivityLogMst
    {
        public int Id { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string? Apiurl { get; set; }
        public string? MethodType { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
    }
}
