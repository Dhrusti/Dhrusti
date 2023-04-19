using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AuthImpl : IAuth
    {
        private readonly AuthBLL _authBLL;

        public AuthImpl(AuthBLL authBLL)
        {
            _authBLL = authBLL;
        }

        public async Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO)
        {
            return await _authBLL.LoginAsync(logInReqDTO);
        }

        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO)
        {
            return _authBLL.GetRefreshToken(refreshTokenReqDTO);
        }

        public string EncryptString(string plainText)
        {
            return _authBLL.EncryptString(plainText);
        }

        public string DecryptString(string cipherText)
        {
            return _authBLL.DecryptString(cipherText);
        }
        public CommonResponse ForgotPassword(ForgotPasswordReqDTO forgotReqDTO)
        {
            return _authBLL.ForgotPassword(forgotReqDTO);
        }
        public CommonResponse ResetPassword(ResetPasswordReqDTO resetPasswordReqDTO)
        {
            return _authBLL.ResetPassword(resetPasswordReqDTO);
        }
        public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqDTO checkResetPasswordLinkReqDTO)
        {
            return _authBLL.CheckResetPasswordLink(checkResetPasswordLinkReqDTO);
        }
        public CommonResponse MobileLoginCheck(MobileLoginApiReqDTO mobileLoginApiReqDTO)
        {
            return _authBLL.MobileLoginCheck(mobileLoginApiReqDTO);
        }
        public CommonResponse MobileApproveReject(MobileApproveRejectReqDTO mobileApproveRejectReqDTO)
        {
            return _authBLL.MobileApproveReject(mobileApproveRejectReqDTO);
        }
    }
}
