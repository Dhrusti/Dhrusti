using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IUploadClientCSVDocument
    {
        public CommonResponse UploadClientCSVDocument(UploadClientCSVDocumentReqDTO uploadClientCSVDataReqDTO);

        public CommonResponse GetAllClientCSVDocumentData(GetAllClientCSVDataReqDTO getAllCSVDataReqDTO);
    }
}
