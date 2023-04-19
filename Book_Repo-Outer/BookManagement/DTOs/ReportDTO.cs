using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ReportDTO
    {
        public int RecordId { get; set; }
        public string BookName { get; set; }
        public string CoverImagePath { get; set; }
        public string AuthorName { get; set; }
        public string Edition { get; set; }
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public int SubCategory { get; set; }
        public string SubCategoryName { get; set; }
        public int BookPages { get; set; }
        public int DownloadCount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
