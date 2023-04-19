using DataLayer.Entities;
using DTO.ResDTO;
using Helper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace Helper
{
    public class AuthRepo
    {
        IConfiguration _iConfiguration;
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonHelper _commonHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepo(IConfiguration iConfiguration, WaltCapitalDBContext dbContext, CommonHelper commonHelper, IHttpContextAccessor httpContextAccessor)
        {
            _iConfiguration = iConfiguration;
            _dbContext = dbContext;
            _commonHelper = commonHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        /*public TokenModel CreateJWT(int UserId, bool IsRefreshTokenExpired)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secreatekey = _iConfiguration["JsonWebTokenKeys:IssuerSigningKey"].ToString();
            var ValidIssuer = _iConfiguration["JsonWebTokenKeys:ValidIssuer"].ToString();
            var ValidAudience = _iConfiguration["JsonWebTokenKeys:ValidAudience"].ToString();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey));
            var claims = new Claim[] {
             new Claim(ClaimTypes.NameIdentifier,UserId.ToString())
             //,
             //new Claim(ClaimTypes.NameIdentifier,ECode.ToString())
            };
            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int TokenExpiryMin = Convert.ToInt32(_iConfiguration.GetSection("JsonWebTokenKeys:TokenExpiryMin").Value);

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

            var status = updatetoken(UserId, writetoken, RefreshToken, IsRefreshTokenExpired);
            if (status != null)
            {
                model.Token = status.Token;                     //writetoken;
                model.RefreshToken = status.RefreshToken;       // RefreshToken;
            }
            return model;
        }*/

        /*public CommonResponse ValidateToken(TokenModel tokenModel)
        {
            CommonResponse response = new CommonResponse();
            response.Message = "Refresh Token Not Generated.";
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secreatekey = _iConfiguration["JsonWebTokenKeys:IssuerSigningKey"].ToString();
                var ValidIssuer = _iConfiguration["JsonWebTokenKeys:ValidIssuer"].ToString();
                var ValidAudience = _iConfiguration["JsonWebTokenKeys:ValidAudience"].ToString();
                var RefreshTokenIsInDays = Convert.ToBoolean(_iConfiguration["JsonWebTokenKeys:RefreshTokenIsInDays"].ToString());
                var RefreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());
                var RefreshTokenExpiryMinutes = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryMinutes"].ToString());

                var claimsPrincipal = tokenHandler.ValidateToken(tokenModel.Token,
                new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = ValidIssuer,
                    ValidateAudience = false,
                    ValidAudience = ValidAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey)),
                    ClockSkew = RefreshTokenIsInDays ? TimeSpan.FromDays(RefreshTokenExpiryDays) : TimeSpan.FromMinutes(RefreshTokenExpiryMinutes),
                    //ClockSkew = TimeSpan.FromDays(RefreshTokenExpiryDays),
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    return response;
                }

                var UserId = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier).Select(_ => _.Value).FirstOrDefault();
                var username = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.Name).Select(_ => _.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(UserId))
                {
                    return response;
                }
                var intUserId = Convert.ToInt32(UserId);

                var user = _dbContext.UserTokenMsts.FirstOrDefault(x => x.UserId == intUserId && x.Token == tokenModel.Token && x.RefreshToken == tokenModel.RefreshToken);

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

                        response.Status = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Refresh Token Generated";
                        response.Data = logInResDTO;
                    }
                }
                else
                {
                    response.Message = "Data not Found or token invalid";
                }
            }
            catch (Exception ex)
            {
                throw;
                //_helper.AddLog("Exception stack :: " + ex.ToString());
                //response.Message = ex.Message;
                //response.Data = ex;
            }
            return response;

        }*/

        /*public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }*/

        /*private UserTokenMst updatetoken(int UserId, string token, string RefreshToken, bool IsRefreshTokenExpired)
        {
            UserTokenMst userTokenMst = new UserTokenMst();
            //bool status = false;
            try
            {
                UserTokenMst tokenmst = new UserTokenMst();
                var UserToken = _dbContext.UserTokenMsts.FirstOrDefault(x => x.UserId == UserId);
                var refreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());
                var RefreshTokenexpiryMinutes = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryMinutes"].ToString());
                var RefreshTokenIsInDays = Convert.ToBoolean(_iConfiguration["JsonWebTokenKeys:RefreshTokenIsInDays"].ToString());

                if (UserToken != null)
                {
                    tokenmst = UserToken;
                    if (IsRefreshTokenExpired)
                    {
                        tokenmst.RefreshToken = RefreshToken;
                        tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        tokenmst.ExpiredOn = RefreshTokenIsInDays ? _commonHelper.GetCurrentDateTime().AddDays(refreshTokenExpiryDays) : _commonHelper.GetCurrentDateTime().AddMinutes(RefreshTokenexpiryMinutes);
                    }
                    else
                    {
                        //tokenmst.Token = token;

                        tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        //tokenmst.ExpiredOn = RefreshTokenIsInDays ? _commonHelper.GetCurrentDateTime().AddDays(refreshTokenExpiryDays) : _commonHelper.GetCurrentDateTime().AddMinutes(RefreshTokenexpiryMinutes);
                    }
                }
                else
                {
                    tokenmst.UserId = UserId;
                    //tokenmst.Token = token;
                    tokenmst.RefreshToken = RefreshToken;
                    tokenmst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    tokenmst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    tokenmst.ExpiredOn = RefreshTokenIsInDays ? _commonHelper.GetCurrentDateTime().AddDays(refreshTokenExpiryDays) : _commonHelper.GetCurrentDateTime().AddMinutes(RefreshTokenexpiryMinutes);
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
            catch (Exception ex)
            {

            }
            return userTokenMst;
        }*/



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
            catch (Exception ex)
            {

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

        public IQueryable GetRoleBasedData(IQueryable source, string? term = "", bool isValidate = true)
        {
            bool isDataSlicingOn = Convert.ToBoolean(Convert.ToString(_iConfiguration["DataSlicingSwitch"]));
            if (isDataSlicingOn)
            {
                if (!isValidate) { return source; }

                var elementType = source.ElementType;

                var stringProperties = elementType.GetProperties().ToArray();
                if (!stringProperties.Any()) { return source; }

                var loggedInUserId = GetLoggedInUserIdAsync();


                var roleDetails = (from UserMst in _dbContext.UserMsts
                                   where UserMst.Id == loggedInUserId
                                   join rm in _dbContext.RoleMsts on UserMst.Role equals Convert.ToString(rm.Id) into rmTemp
                                   from RoleMst in rmTemp.DefaultIfEmpty()
                                   select new { UserMst, RoleMst }).FirstOrDefault();

                StringBuilder filterExpr = new StringBuilder();

                if (roleDetails == null || roleDetails.UserMst == null || roleDetails.RoleMst == null)
                {
                    if (roleDetails != null && roleDetails.UserMst != null)
                    {
                        if (roleDetails.UserMst.AccessCategoryId == 1)
                        {

                        }
                    }
                    else
                    {
                        filterExpr.Append("Id.Equals(0)");
                        return source.Where(filterExpr.ToString(), term);
                    }
                }

                if (roleDetails!.UserMst.AccessCategoryId != 3) { return source; }

                string roleName = roleDetails!.RoleMst!.RoleName;

                if (roleName == Roles.Branch_Manager.ToString().Replace("_", " "))
                {
                    string var1 = Convert.ToString(roleDetails.UserMst.Office);
                    filterExpr.Append(filterExpr.Length > 0 ? filterExpr.Append(" && Office.Equals(" + var1 + ")") : "Office.Equals(" + var1 + ")");
                    term += term!.Concat("Office");
                }
                else if (roleName == Roles.Portfolio_Manager.ToString().Replace("_", " "))
                {
                    string var1 = loggedInUserId.ToString();
                    filterExpr.Append(filterExpr.Length > 0 ? filterExpr.Append(" && Ifa.Equals(" + var1 + ")") : "Ifa.Equals(" + var1 + ")");
                    term += term!.Concat("Ifa");
                }
                else
                {
                    filterExpr.Append("IsDeleted == false");
                }


                return source.Where(filterExpr.ToString(), term);
            }
            else
                return source;
        }

        public int GetLoggedInUserIdAsync()
        {
            string accessToken = Convert.ToString(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]) ?? "";

            if (string.IsNullOrEmpty(accessToken)) { return 0; }
            accessToken = accessToken.Replace("Bearer ", "").Trim();

            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = GetUserIdFromToken(accessToken, out validatedToken);

            var UserId = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier).Select(_ => _.Value).FirstOrDefault();

            return Convert.ToInt32(UserId);
        }
    }
}
