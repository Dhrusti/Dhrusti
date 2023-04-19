using BusinessLayer;
using DTO.ReqDTO;
using DTO.ResDTO;
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

        public CommonResponse UploadCSVDocument(UploadCSVDataDocumentReqDTO uploadCSVDataDocumentReqDTO)
        {
            return _uploadCSVDocumentBLL.UploadCSVData(uploadCSVDataDocumentReqDTO);
        }
        //public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO)
        //{
        //    return _uploadCSVDocumentBLL.GetAllCSVDocumentData(getAllCSVDataReqDTO);
        //}
        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataDocumentReqDTO getAllCSVDataDocumentReqDTO)
        {
            return _uploadCSVDocumentBLL.GetAllCSVDocumentData(getAllCSVDataDocumentReqDTO);
        }
    }
}
