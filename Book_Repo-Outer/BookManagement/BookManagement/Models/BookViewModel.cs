

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class BookViewModel
    {
        public BookViewModel()
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
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? BookName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? AuthorName { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Value must be between 1 to 1000")]
        public int BookPages { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? Publisher { get; set; }
        [Required]
        public DateTime? PublishDate { get; set; }
        [Required]
        public string? Edition { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "1000000")]
        public decimal Price { get; set; }
        [Required]
        public string CoverImagePath { get; set; }
        [Required]
        public string Pdfpath { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
