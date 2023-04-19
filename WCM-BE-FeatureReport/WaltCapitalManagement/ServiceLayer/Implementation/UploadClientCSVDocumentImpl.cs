using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class UploadClientCSVDocumentImpl : IUploadClientCSVDocument
    {
        private readonly UploadClientCSVDocumentBLL _uploadClientCSVDocumentBLL;

        public UploadClientCSVDocumentImpl(UploadClientCSVDocumentBLL uploadClientCSVDocumentBLL)
        {
            _uploadClientCSVDocumentBLL = uploadClientCSVDocumentBLL;
        }

        public CommonResponse UploadClientCSVDocument(UploadClientCSVDocumentReqDTO uploadclientCSVDocumentDTO)
        {
            return _uploadClientCSVDocumentBLL.UploadClientCSVDocument(uploadclientCSVDocumentDTO);
        }
        public CommonResponse GetAllClientCSVDocumentData(GetAllClientCSVDataReqDTO getAllClientCSVDataReqDTO)
        {
            return _uploadClientCSVDocumentBLL.GetAllClientCSVDocumentData(getAllClientCSVDataReqDTO);
        }

    }
}
