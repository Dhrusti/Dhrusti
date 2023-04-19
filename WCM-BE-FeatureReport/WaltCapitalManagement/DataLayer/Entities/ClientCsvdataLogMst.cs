using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ClientCsvdataLogMst
    {
        public int Id { get; set; }
        public string ClientCsvfileName { get; set; } = null!;
        public string FileSize { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string? Path { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
