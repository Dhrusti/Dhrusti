using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class CarMst
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public int RegistrationId { get; set; }
        public int Price { get; set; }
        public int? Brand { get; set; }
        public DateTime BuyTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
