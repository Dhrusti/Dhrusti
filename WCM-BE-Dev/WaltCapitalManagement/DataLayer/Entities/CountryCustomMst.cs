using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class CountryCustomMst
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
