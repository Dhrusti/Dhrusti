using BussinessLayer;
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

        public CommonResponse Login(LoginReqDTO loginReqDTO)
        {
            return _authBLL.Login(loginReqDTO);
        }
        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO)
        {
            return _authBLL.GetRefreshToken(refreshTokenReqDTO);
        }
    }
}
