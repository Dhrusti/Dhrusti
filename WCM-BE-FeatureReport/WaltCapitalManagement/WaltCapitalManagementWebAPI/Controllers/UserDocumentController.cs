
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
    public class UserDocumentController : ControllerBase
    {
        private readonly IUserDocument _iUserDocument;
        public UserDocumentController(IUserDocument iUserDocument)
        {
            _iUserDocument = iUserDocument;
        }

        [HttpPost("AddUserDocument")]
        [Consumes("multipart/form-data")]
        public CommonResponse AddUserDocument([FromForm] UserDocumentReqViewModel userDocumentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUserDocument.AddUserDocument(userDocumentReqViewModel.Adapt<UserDocumentReqDTO>());
                UserDocumentResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UserDocumentResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("RemoveUserDocument")]
        [Consumes("multipart/form-data")]
        public CommonResponse RemoveUserDocument([FromForm] DeleteAccountTypeReqViewModel deleteAccountTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUserDocument.RemoveUserDocument(deleteAccountTypeReqViewModel.Adapt<DeleteAccountTypeReqDTO>());
                DeleteAccountTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteAccountTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
