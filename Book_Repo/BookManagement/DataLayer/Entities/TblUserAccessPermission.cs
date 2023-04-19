using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TblUserAccessPermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Permissionid { get; set; }
        public int AccessId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
