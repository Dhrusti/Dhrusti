using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class LogInResDTO
    {
        public dynamic UserDetail { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
     
    }
    public class UserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string Role { get; set; }


    }
}
