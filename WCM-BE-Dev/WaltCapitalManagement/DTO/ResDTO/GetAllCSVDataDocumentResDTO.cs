using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllCSVDataDocumentResDTO
    {
        public int TotalCount { get; set; }
        public List<CSVDataDocumentDetails> CSVDataDocumentList { get; set; }

    }
    public class CSVDataDocumentDetails
    {
        public int Id { get; set; }
        public string ServiceProviderName { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
    }
}
