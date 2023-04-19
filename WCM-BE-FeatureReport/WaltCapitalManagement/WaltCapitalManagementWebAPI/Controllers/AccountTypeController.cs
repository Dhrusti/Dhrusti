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
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountType _iaccountType;

        public AccountTypeController(IAccountType iaccountType)
        {
            _iaccountType = iaccountType;
        }

        [HttpPost("GetAllAccountType")]
        public CommonResponse GetAllAccountType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccountType.GetAllAccountType();
                List<GetAccountTypeResDTO> getAccountTypeResDTO = commonResponse.Data ?? new List<GetAccountTypeResDTO>();
                commonResponse.Data = getAccountTypeResDTO.Adapt<List<GetAccountTypeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("GetAccountTypeById")]
        public CommonResponse GetAccountTypeById(GetAccountTypeReqViewModel getAccountTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccountType.GetAccountTypeById(getAccountTypeReqViewModel.Adapt<GetAccountTypeReqDTO>());
                GetAccountTypeResDTO getAccountTypeResDTO = commonResponse.Data ?? new GetAccountTypeResDTO();
                commonResponse.Data = getAccountTypeResDTO.Adapt<GetAccountTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("AddAccountType")]
        public CommonResponse AddAccountType(AddAccountTypeReqViewModel addAccountTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccountType.AddAccountType(addAccountTypeReqViewModel.Adapt<AddAccountTypeReqDTO>());
                AddAccountTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddAccountTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("UpdateAccountType")]
        public CommonResponse UpdateAccountType(UpdateAccountTypeReqViewModel updateAccountTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccountType.UpdateAccountType(updateAccountTypeReqViewModel.Adapt<UpdateAccountTypeReqDTO>());
                UpdateAccountTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateAccountTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("DeleteAccountType")]
        public CommonResponse DeleteAccountType(DeleteAccountTypeReqViewModel deleteAccountTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iaccountType.DeleteAccountType(deleteAccountTypeReqViewModel.Adapt<DeleteAccountTypeReqDTO>());
                DeleteAccountTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteAccountTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
