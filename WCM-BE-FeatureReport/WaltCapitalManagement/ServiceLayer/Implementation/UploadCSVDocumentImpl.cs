using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class UploadCSVDocumentImpl : IUploadCSVDocument
    {
        private readonly UploadCSVDocumentBLL _uploadCSVDocumentBLL;

        public UploadCSVDocumentImpl(UploadCSVDocumentBLL uploadCSVDocumentBLL)
        {
            _uploadCSVDocumentBLL = uploadCSVDocumentBLL;
        }

        public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO)
        {
            return _uploadCSVDocumentBLL.UploadCSVDocument(uploadDocumentReqDTO);
        }
        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO)
        {
            return _uploadCSVDocumentBLL.GetAllCSVDocumentData(getAllCSVDataReqDTO);
        }
    }
}
