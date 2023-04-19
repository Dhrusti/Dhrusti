namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UploadCSVPPMDocumentReqViewModel
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public int ServiceProviderId { get; set; }
    }
}
