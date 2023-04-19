using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class UserMstModel
    {
        public int UserId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public int RoleId { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [Display(Name = "Contact Number")]
        [MaxLength(10)]
       
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
