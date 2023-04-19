using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace BookManagement.Models
{
    public class EditBookModel
    {
        //public EditBookModel()
        //{
        //    this.Category = new List<SelectListItem>();
        //    this.SubCategory = new List<SelectListItem>();

        //}

        //public List<SelectListItem> Category { get; set; }
        //public List<SelectListItem> SubCategory { get; set; }


        public int BookId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "The Category Field is required")]
        public int CategoryId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "The SubCategory Field is required")]
        public int SubCategoryId { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [MinLength(3, ErrorMessage = "please Enter atleast 3 Letter..")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40 Letter..")]
        public string? BookName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [MinLength(3, ErrorMessage = "please Enter atleast 3 Letter..")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40 Letter..")]
        public string? AuthorName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "please Enter atleast 3 digit number..")]
        [MaxLength(6, ErrorMessage = "please don't Enter more than 6 digit number..")]
        //[Range(1, 100000, ErrorMessage = "Value must be between 1 to 100000")]
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
        //[Range(10, 1000000, ErrorMessage = "Value must be between 1 to 1000000")]
        //[MinLength(3, ErrorMessage = "please Enter atleast 3 digit number..")]
        //[MaxLength(6, ErrorMessage = "please don't Enter more than 6 digit number..")]
        public decimal Price { get; set; }
      
        //public string CoverImagePath { get; set; }
        //[Required]
        //public string Pdfpath { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}
