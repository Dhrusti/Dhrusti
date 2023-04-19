using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUploadDocument
    {
        public CommonResponse UploadCSVDocument(UploadCSVDocumentReqDTO uploadDocumentReqDTO);

        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO);
    }
}
