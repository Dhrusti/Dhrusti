using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUploadCSVDocument
    {
        public CommonResponse UploadCSVDocument(UploadCSVDataDocumentReqDTO uploadCSVDataDocumentReqDTO);
        //public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqDTO getAllCSVDataReqDTO);
        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataDocumentReqDTO getAllCSVDataDocumentReqDTO);

    }
}
