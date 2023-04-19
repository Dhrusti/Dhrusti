using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class CityCustomMst
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int CreatedBy { get; set; }
    }
}
