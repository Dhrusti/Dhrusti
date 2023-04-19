namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UploadClientCSVDocumentReqViewModel
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
