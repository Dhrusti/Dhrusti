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
    public class UploadClientCSVDocumentController : Controller
    {
        private readonly IUploadClientCSVDocument _iUploadClientCSVDocument;
        public UploadClientCSVDocumentController(IUploadClientCSVDocument iUploadClientCSVDocument)
        {
            _iUploadClientCSVDocument = iUploadClientCSVDocument;
        }

        [HttpPost("UploadClientCSVDocument")]
        public CommonResponse UploadClientCSVDocument([FromForm] UploadClientCSVDocumentReqViewModel uploadClientCSVDocumentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUploadClientCSVDocument.UploadClientCSVDocument(uploadClientCSVDocumentReqViewModel.Adapt<UploadClientCSVDocumentReqDTO>());

            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllClientCSVDocumentData")]
        public CommonResponse GetAllClientCSVDocumentData(GetAllClientCSVDataReqViewModel getAllClientCSVDataReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUploadClientCSVDocument.GetAllClientCSVDocumentData(getAllClientCSVDataReqViewModel.Adapt<GetAllClientCSVDataReqDTO>());
                GetAllClientCSVDocumnetResDTO ClientCSVDocumentResDTO = commonResponse.Data ?? new GetAllClientCSVDocumnetResDTO();
                commonResponse.Data = ClientCSVDocumentResDTO.Adapt<GetAllClientCSVDocumnetResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
