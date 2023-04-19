using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class BookModel
    {
        public BookModel()
        {
            this.Category = new List<SelectListItem>();
            this.SubCategory = new List<SelectListItem>();

        }

        public List<SelectListItem> Category { get; set; }
        public List<SelectListItem> SubCategory { get; set; }


        public int BookId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "The Category Field is required")]
        public int CategoryId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "The SubCategory Field is required")]
        public int SubCategoryId { get; set; }


        [Required]
        
        [MinLength(3, ErrorMessage = "please Enter atleast 3  Letter....")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40  Letter....")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? BookName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        
        [MinLength(3, ErrorMessage = "please Enter atleast 3  Letter..")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40 Letter..")]
        public string? AuthorName { get; set; }
        [Required]
    
        //[Range(1, 1000, ErrorMessage = "Value must be between 1 to 1000")]
        [MinLength(2,ErrorMessage = "please Enter atleast 2 digit number..")]
        [MaxLength(8, ErrorMessage = "please don't Enter more than 8 digit number..")]
        //[Range(1, 1000, ErrorMessage = "Value must be between 1 to 1000")]
        public int BookPages { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
    
        [MinLength(3, ErrorMessage = "please Enter atleast 3 Letter..")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40 Letter..")]
        public string? Publisher { get; set; }
        [Required]

        public DateTime? PublishDate { get; set; }
        [Required]
        public string? Edition { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        // [Range(typeof(decimal), "0", "1000")]
        public decimal Price { get; set; }
        [Required]
        public IFormFile CoverImagePath { get; set; }
        [Required]
        public IFormFile Pdfpath { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }





    }
}