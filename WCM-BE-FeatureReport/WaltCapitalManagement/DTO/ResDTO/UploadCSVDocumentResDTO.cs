namespace DTO.ResDTO
{
    public class UploadCSVDocumentResDTO
    {
        public int UserId { get; set; }
        public int CSVDocumentTypeId { get; set; }
        public string CSVDocumentPath { get; set; }
        public dynamic Files { get; set; }
    }
}
