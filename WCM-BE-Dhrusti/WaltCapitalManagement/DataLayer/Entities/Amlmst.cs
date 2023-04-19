using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Amlmst
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
