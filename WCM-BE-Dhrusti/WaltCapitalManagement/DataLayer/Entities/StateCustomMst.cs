using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class StateCustomMst
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public int CreatedBy { get; set; }
    }
}
