using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class BookDownloadModel
    {
        public int BookUserId { get; set; }
        public string BookName { get; set; }
        public string? Publisher { get; set; }
        public decimal price { get; set; }
        public string pdfpath { get; set; }
        public string coverimage { get; set; }
        public string AuthorName { get; set; }
        public int BookId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage ="Please Enter your First")]
        public string? FirstName { get; set; }
        [Display(Name ="Last Name")]
        [Required]
        public string? LastNane { get; set; }
        
        [Required]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage = "Please enter correct email...")]

        public string? EmailId { get; set; }
        [Display(Name = "Contact Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Location { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class BookDownloadStringModel
    {
        public string BookUserId { get; set; }
        public string BookName { get; set; }
        public string? Publisher { get; set; }
        public string price { get; set; }
        public string pdfpath { get; set; }
        public string AuthorName { get; set; }
        public string BookId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter your First")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string? LastNane { get; set; }

        [Required]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage = "Please enter correct email...")]

        public string? EmailId { get; set; }
        [Display(Name = "Contact Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Location { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdateBy { get; set; }
        //public string CreatedOn { get; set; }
        //public string UpdateOn { get; set; }
        //public string? IsActive { get; set; }
        //public string IsDeleted { get; set; }
    }
}
