using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class SubCategoryViewModel
    {

        public int SubCategoryId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "The Category Field is required")]
        public int CategoryId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [MinLength(3, ErrorMessage = "please Enter atleast 3  Letter....")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40  Letter....")]
        [Required(ErrorMessage = "SubCategory Field is required")]
        public string? SubCategoryName { get; set; }
       
       // public string? CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
