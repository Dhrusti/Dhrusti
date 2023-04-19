using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class StateMst
    {
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
