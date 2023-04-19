namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UploadDocumentReqViewModel
    {
        public int UserId { get; set; }
        public IFormFile Files { get; set; }
    }
}
