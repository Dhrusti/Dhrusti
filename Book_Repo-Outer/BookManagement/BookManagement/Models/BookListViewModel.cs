using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookManagement.Models
{
    public class BookListViewModel
    {
        public BookListViewModel()
        {
            this.Category = new List<SelectListItem>();
            this.SubCategory = new List<SelectListItem>();
            this.Mainmodel = new List<MainModel>();

        }

        public List<SelectListItem> Category { get; set; }
        public List<SelectListItem> SubCategory { get; set; }
        public List<MainModel> Mainmodel { get; set; }
        public class MainModel
        {

            public int BookId { get; set; }
            public int CategoryId { get; set; }
            public int SubCategoryId { get; set; }
            public string? BookName { get; set; }
            public string? AuthorName { get; set; }
            public int BookPages { get; set; }
            public string? Publisher { get; set; }
            public DateTime? PublishDate { get; set; }
            public string? Edition { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public string? CoverImagePath { get; set; }
            public string? Pdfpath { get; set; }
            public int CreatedBy { get; set; }
            public int UpdateBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime UpdateOn { get; set; }
            public bool? IsActive { get; set; }
            public bool IsDeleted { get; set; }
        }
    }

}
