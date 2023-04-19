using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Interface;
//using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
       
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<CommonResponse> Login(LogInReqViewModel logInReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _auth.LoginAsync(logInReqViewModel.Adapt<LogInReqDTO>());
                LogInResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<LogInResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        [AllowAnonymous]
        [HttpPost("GetRefreshToken")]
        public CommonResponse GetRefreshToken(GetRefreshTokenReqViewModel refreshTokenReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.GetRefreshToken(refreshTokenReqViewModel.Adapt<GetRefreshTokenReqDTO>());
                LogInResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<LogInResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetEncryption")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetEncryption(EncryptDecryptReqViewModel encryptDecryptReqViewModel)
        {
            return _auth.EncryptString(encryptDecryptReqViewModel.Text);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetDecryption")]
        [ApiExplorerSettings(IgnoreApi = true)]

        public string GetDecryption(EncryptDecryptReqViewModel encryptDecryptReqViewModel)
        {
            return _auth.DecryptString(encryptDecryptReqViewModel.Text);
        }

    }
}
