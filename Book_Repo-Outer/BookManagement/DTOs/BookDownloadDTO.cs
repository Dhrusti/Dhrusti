using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BookDownloadDTO
    {
        public int BookUserId { get; set; }
        public string BookName { get; set; }
        public string? Publisher { get; set; }
        public decimal price { get; set; }

        public string pdfpath { get; set; }
        public string AuthorName { get; set; }
        public int BookId { get; set; }
        public string? FirstName { get; set; }
        public string? LastNane { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }
        public string? Location { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
