using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadCSVDocumentController : ControllerBase
    {
        private readonly IUploadCSVDocument _iUploadCSVDocument;
        public UploadCSVDocumentController(IUploadCSVDocument iUploadCSVDocument)
        {
            this._iUploadCSVDocument = iUploadCSVDocument;
        }

        [HttpPost("UploadDocument")]
        public CommonResponse UploadCSVDocument([FromForm] UploadCSVDocumentReqViewModel uploadDocumentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUploadCSVDocument.UploadCSVDocument(uploadDocumentReqViewModel.Adapt<UploadCSVDocumentReqDTO>());
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllCSVDocumentData")]

        public CommonResponse GetAllCSVDocumentData(GetAllCSVDataReqViewModel getAllCSVDataReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUploadCSVDocument.GetAllCSVDocumentData(getAllCSVDataReqViewModel.Adapt<GetAllCSVDataReqDTO>());
                GetAllCSVDocumnetResDTO CSVDocumentResDTO = commonResponse.Data ?? new GetAllCSVDocumnetResDTO();
                commonResponse.Data = CSVDocumentResDTO.Adapt<GetAllCSVDocumnetResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
