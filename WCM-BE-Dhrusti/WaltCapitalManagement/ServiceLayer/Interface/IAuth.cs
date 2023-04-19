using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAuth
    {
        public Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO);
        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO);
        public string EncryptString(string plainText);
        public string DecryptString(string cipherText);
        public CommonResponse ForgotPassword(ForgotPasswordReqDTO forgotReqDTO);
        public CommonResponse ResetPassword(ResetPasswordReqDTO resetPasswordReqDTO);
        public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqDTO checkResetPasswordLinkReqDTO);
        public CommonResponse MobileLoginCheck(MobileLoginApiReqDTO mobileLoginApiReqDTO);
        public CommonResponse MobileApproveReject(MobileApproveRejectReqDTO mobileApproveRejectReqDTO);
    }
}
