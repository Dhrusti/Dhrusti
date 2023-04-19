using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserMstDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string? ResetCode { get; set; }
    }
}
