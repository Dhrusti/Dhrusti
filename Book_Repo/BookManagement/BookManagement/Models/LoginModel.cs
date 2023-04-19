using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int RoleId { get; set; }

        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
