using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CommonResponse GetRefreshToken(GetRefreshTokenReqDTO refreshTokenReqDTO)
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

    }
}
