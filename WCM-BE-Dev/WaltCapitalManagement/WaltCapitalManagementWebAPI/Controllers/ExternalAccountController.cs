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
    public class ExternalAccountController : ControllerBase
    {
        private readonly IExternalAccount _iexternalAccount;

        public ExternalAccountController(IExternalAccount externalAccount)
        {
            this._iexternalAccount = externalAccount;
        }

        [HttpPost("AddExternalAccount")]
        public CommonResponse AddExternalAccount(AddExternalAccountReqViewModel addExternalAccountReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iexternalAccount.AddExternalAccount(addExternalAccountReqViewModel.Adapt<AddExternalAccountReqDTO>());
                AddExternalAccountResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddExternalAccountResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllExternalAccountByUserId")]
        public CommonResponse GetAllExternalAccountByUserId(GetAllExternalAccountReqViewModel getAllExternalAccountReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iexternalAccount.GetAllExternalAccountByUserId(getAllExternalAccountReqViewModel.Adapt<GetAllExternalAccountReqDTO>());
                List<GetAllExternalAccountResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetExternalAccountResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteExternalAccount")]
        public CommonResponse DeleteExternalAccount(DeleteExternalAccountReqViewModel deleteExternalAccountReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iexternalAccount.DeleteExternalAccount(deleteExternalAccountReqViewModel.Adapt<DeleteExternalAccountReqDTO>());
                DeleteExternalAccountResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteExternalAccountResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateExternalAccount")]
        public CommonResponse UpdateExternalAccount(UpdateExternalAccountReqViewModel updateExternalAccountReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iexternalAccount.UpdateupdateExternalAccount(updateExternalAccountReqViewModel.Adapt<UpdateExternalAccountReqDTO>());
                UpdateExternalAccountResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateExternalAccountResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
