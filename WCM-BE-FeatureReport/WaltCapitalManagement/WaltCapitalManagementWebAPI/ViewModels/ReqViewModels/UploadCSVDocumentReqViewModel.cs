namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UploadCSVDocumentReqViewModel
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
