using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "New and Confirm passwords are different")]
        [Compare("NewPassword")]
        
        public string ConfirmPassword { get; set; }

        public string ResetCode { get; set; }
    }
}
