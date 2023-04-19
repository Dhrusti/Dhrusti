using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAuth
    {
        public Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO);
        public CommonResponse GetRefreshToken(GetRefreshTokenReqDTO refreshTokenReqDTO);

        public string EncryptString(string plainText);
        public string DecryptString(string cipherText);
    }
}
