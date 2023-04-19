using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAuth
    {
        public CommonResponse Login(LoginReqDTO loginReqDTO);
        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO);
    }
}
