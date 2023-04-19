using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUploadCSVDocument
    {
        public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO);
        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO);
    }
}
