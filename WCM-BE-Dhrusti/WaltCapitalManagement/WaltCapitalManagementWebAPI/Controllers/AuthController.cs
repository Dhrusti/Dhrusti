using DTO;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.Hubs;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly INotification _notification;
        private readonly IHubContext<ChatHub> _chatClient;
        public AuthController(IAuth auth, INotification notification, IHubContext<ChatHub> chatHub)
        {
            _auth = auth;
            _notification = notification;
            _chatClient = chatHub;
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
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [AllowAnonymous]
        [HttpPost("GetRefreshToken")]
        public CommonResponse GetRefreshToken(RefreshTokenReqViewModel refreshTokenReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.GetRefreshToken(refreshTokenReqViewModel.Adapt<RefreshTokenReqDTO>());
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

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public CommonResponse ForgotPassword(ForgotPasswordReqViewModel forgotReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.ForgotPassword(forgotReqViewModel.Adapt<ForgotPasswordReqDTO>());

                ForgotPasswordResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<ForgotPasswordResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public CommonResponse ResetPassword(ResetPasswordReqViewModel resetPasswordReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.ResetPassword(resetPasswordReqViewModel.Adapt<ResetPasswordReqDTO>());

                var list = commonResponse.Data;
                commonResponse.Data = list;
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("CheckResetPasswordLink")]
        [AllowAnonymous]
        public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqViewModel checkResetPasswordLinkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.CheckResetPasswordLink(checkResetPasswordLinkReqViewModel.Adapt<CheckResetPasswordLinkReqDTO>());

                var list = commonResponse.Data;
                commonResponse.Data = list;
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [AllowAnonymous]
        [HttpPost("MobileLoginApi")]
        public CommonResponse MobileLoginApi(MobileLoginApiReqViewModel mobileLoginApiReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.MobileLoginCheck(mobileLoginApiReqViewModel.Adapt<MobileLoginApiReqDTO>());
                MobileLoginApiResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<MobileLoginApiResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [AllowAnonymous]
        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(NotificationReqViewModel notificationReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _notification.SendNotification(notificationReqViewModel.Adapt<NotificationReqDTO>());
                MobileLoginApiResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<MobileLoginApiResViewModel>();
            }
            catch (Exception) { throw; }
            return Ok(commonResponse);
        }

        [AllowAnonymous]
        [HttpPost("MobileApproveReject")]
        public CommonResponse MobileApproveRejectAsync(MobileApproveRejectReqViewModel mobileApproveRejectReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.MobileApproveReject(mobileApproveRejectReqViewModel.Adapt<MobileApproveRejectReqDTO>());
                var list = commonResponse.Data;
                commonResponse.Data = list;
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [AllowAnonymous]
        [HttpPost("SignalRTest")]
        public async Task<CommonResponse> SignalRTest(MobileApproveRejectReqViewModel mobileApproveRejectReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                ChatHub chatHub = new ChatHub(_chatClient);
                ChatMessage chatMessage = new ChatMessage();
                chatMessage.User = "Tanmay";
                chatMessage.Message = "Message From .NET";
                await chatHub.SendMessage(chatMessage);

                //await _chatClient.Clients.All.SendMessage(chatMessage);

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Message = "Success";
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
