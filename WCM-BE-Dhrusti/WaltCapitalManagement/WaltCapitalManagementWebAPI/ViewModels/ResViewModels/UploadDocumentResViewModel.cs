namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class UploadDocumentResViewModel
    {
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentPath { get; set; }
        public IFormFile Files { get; set; }
    }
}
