using DataLayer.Entities;
using DTO.ResDTO;
using Helper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class AuthRepo
    {
        IConfiguration _iConfiguration;
         private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonHelper _commonHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepo(IConfiguration iConfiguration, MedicalBillingDbContext dbContext, CommonHelper commonHelper, IHttpContextAccessor httpContextAccessor)
        {
            _iConfiguration = iConfiguration;
            _dbContext = dbContext;
            _commonHelper = commonHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        public TokenModel CreateJWT(int UserName, bool IsRefreshTokenExpired)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secreatekey = _iConfiguration["JsonWebTokenKeys:IssuerSigningKey"].ToString();
            var ValidIssuer = _iConfiguration["JsonWebTokenKeys:ValidIssuer"].ToString();
            var ValidAudience = _iConfiguration["JsonWebTokenKeys:ValidAudience"].ToString();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey));
            var claims = new Claim[] {
             new Claim(ClaimTypes.NameIdentifier,UserName.ToString())
             //,
             //new Claim(ClaimTypes.NameIdentifier,ECode.ToString())
            };
            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int TokenExpiryMin = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:TokenExpiryMin"]);

            var newJwtToken = new JwtSecurityToken(
                  issuer: ValidIssuer,
                  audience: ValidAudience,
                  notBefore: _commonHelper.GetCurrentDateTime(),
                  expires: _commonHelper.GetCurrentDateTime().AddMinutes(TokenExpiryMin),
                  signingCredentials: signingcredentials,
                  claims: claims
            );

            var writetoken = tokenHandler.WriteToken(newJwtToken);
            var RefreshToken = CreateRefreshToken();

            TokenModel model = new TokenModel();

            var status = updatetoken(UserName, writetoken, RefreshToken, IsRefreshTokenExpired);
            if (status != null)
            {
                model.Token = status.Token;                     //writetoken;
                model.RefreshToken = status.RefreshToken;       // RefreshToken;
            }
            return model;
        }

        public CommonResponse ValidateToken(TokenModel tokenModel)
        {
            CommonResponse response = new CommonResponse();
            response.Message = "Refresh Token Not Generated.";
            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = GetUserIdFromToken(tokenModel.Token, out validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    return response;
                }

                var UserId = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.Name).Select(_ => _.Value).FirstOrDefault();
                var username = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier).Select(_ => _.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(username))
                {
                    return response;
                }
                //var intUserId = Convert.ToInt32(UserId);

                var user = _dbContext.UserTokenMsts.FirstOrDefault(x => x.UserId == Convert.ToInt32(username) && x.Token == tokenModel.Token && x.RefreshToken == tokenModel.RefreshToken);

                LogInResDTO logInResDTO = new LogInResDTO();
                if (user != null)
                {
                    bool IsRefreshTokenExpired = false;
                    if (user.ExpiredOn < _commonHelper.GetCurrentDateTime())
                    {
                        IsRefreshTokenExpired = true;
                    }
                    var res = CreateJWT(user.UserId, IsRefreshTokenExpired);
                    if (res != null)
                    {
                        logInResDTO.Token = res.Token;
                        logInResDTO.RefreshToken = res.RefreshToken;

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Refresh Token Generated";
                        response.Data = logInResDTO;
                    }
                }
                else
                {
                    response.Message = "Data not Found or token invalid";
                    response.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            catch (Exception ex)
            {
                //_helper.AddLog("Exception stack :: " + ex.ToString());
                response.StatusCode = HttpStatusCode.ExpectationFailed;
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;

        }

        private ClaimsPrincipal GetUserIdFromToken(string token, out SecurityToken validatedToken)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();
            var secreatekey = _iConfiguration["JsonWebTokenKeys:IssuerSigningKey"].ToString();
            var ValidIssuer = _iConfiguration["JsonWebTokenKeys:ValidIssuer"].ToString();
            var ValidAudience = _iConfiguration["JsonWebTokenKeys:ValidAudience"].ToString();
            var RefreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());

            claimsPrincipal = tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ValidIssuer,
                ValidateAudience = true,
                ValidAudience = ValidAudience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey)),
                ClockSkew = TimeSpan.FromDays(RefreshTokenExpiryDays),
                ValidateLifetime = true
            }, out validatedToken);

            return claimsPrincipal;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private UserTokenMst updatetoken(int UserName, string token, string RefreshToken, bool IsRefreshTokenExpired)
        {
            UserTokenMst userTokenMst = new UserTokenMst();
            try
            {
                UserTokenMst tokenmst = new UserTokenMst();
                var UserToken = _dbContext.UserTokenMsts.FirstOrDefault(x => x.UserId == Convert.ToInt32(UserName));
                var refreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());

                if (UserToken != null)
                {
                    tokenmst = UserToken;
                    if (IsRefreshTokenExpired)
                    {
                        tokenmst.RefreshToken = RefreshToken;
                        tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        tokenmst.ExpiredOn = _commonHelper.GetCurrentDateTime().AddDays(refreshTokenExpiryDays);
                    }
                    else
                    {
                        //tokenmst.Token = token;
                        tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    }
                }
                else
                {
                    tokenmst.UserId = Convert.ToInt32(UserName);
                    tokenmst.RefreshToken = RefreshToken;
                    tokenmst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    tokenmst.ExpiredOn = _commonHelper.GetCurrentDateTime().AddDays(refreshTokenExpiryDays);
                }
                tokenmst.Token = token;

                if (UserToken != null)
                {
                    _dbContext.Entry(tokenmst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.UserTokenMsts.Add(tokenmst);
                    _dbContext.SaveChanges();
                }
                userTokenMst = tokenmst;
            }
            catch (Exception)
            {
                throw;
            }
            return userTokenMst;
        }

        public string EncryptString(string plainText)
        {
            return _commonHelper.EncryptString(plainText);
        }

        public string DecryptString(string cipherText)
        {
            return _commonHelper.DecryptString(cipherText);
        }

    }
}
