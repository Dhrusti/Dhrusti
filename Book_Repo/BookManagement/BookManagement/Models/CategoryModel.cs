using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.Category = new List<SelectListItem>();
            this.SubCategory = new List<SelectListItem>();

        }

        public List<SelectListItem> Category { get; set; }
        public List<SelectListItem> SubCategory { get; set; }


        public int CategoryId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "please Enter atleast 3  Letter....")]
        [MaxLength(40, ErrorMessage = "please don't Enter more than 40  Letter....")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
