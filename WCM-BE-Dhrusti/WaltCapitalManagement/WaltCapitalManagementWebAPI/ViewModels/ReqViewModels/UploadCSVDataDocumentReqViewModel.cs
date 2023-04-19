namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UploadCSVDataDocumentReqViewModel
    {
        public int? UserId { get; set; }
        public int? ServiceProviderId { get; set; }
        public IFormFile File { get; set; }
    }
}
