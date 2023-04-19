namespace DTO.ResDTO
{
    public class UploadClientCSVDocumentResDTO
    {
        public int UserId { get; set; }
        public int ClientCSVDocumentTypeId { get; set; }
        public string ClientCSVDocumentPath { get; set; }
        public dynamic Files { get; set; }
    }
}
