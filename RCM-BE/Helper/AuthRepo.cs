using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class AuthRepo
    {
        IConfiguration _iConfiguration;
        private readonly CommonHelper _commonHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepo(IConfiguration iConfiguration, CommonHelper commonHelper, IHttpContextAccessor httpContextAccessor)
        {
            _iConfiguration = iConfiguration;
            _commonHelper = commonHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public CommonResponse ValidateUser(LoginReqDTO loginReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                LoginResDTO loginResDTO = new LoginResDTO();
                commonResponse.Data = loginResDTO;
                string UniqueId = "1";
                string UserName = "Admin";
                string Password = "Admin";
                if (loginReqDTO.UserName == UserName && loginReqDTO.Password == Password)
                {
                    int dataCount = 0;
                    //var tokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"].ToString());
                    //var refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"].ToString());
                    string FilePath = Path.Combine(_commonHelper.GetPhysicalRootPath(), "UserToken", "UserTokenData.json");
                    var userTockenDataModelList = new List<UserTockenDataModel>();
                    string jsonData = string.IsNullOrWhiteSpace(_commonHelper.ReadJsonFile(FilePath)) ? "[]" : _commonHelper.ReadJsonFile(FilePath);
                    userTockenDataModelList = JsonConvert.DeserializeObject<List<UserTockenDataModel>>(jsonData);
                    dataCount = userTockenDataModelList.Count();
                    UserTockenDataModel UserToken = userTockenDataModelList.FirstOrDefault(x => x.UserId == UniqueId);

                    if (UserToken != null)
                    {
                        //Generate JWT Token
                        var Token = GenerateToken(UniqueId);
                        if (UserToken.RefreshTockenExpiredOn < _commonHelper.GetCurrentDateTime()) // IF Refresh Token Expired == True
                        {
                            //Generate Refresh Token
                            var RefreshToken = CreateRefreshToken();
                            commonResponse = AddUpdateToken(UniqueId, Token, RefreshToken, true);
                        }
                        else
                        {
                            commonResponse = AddUpdateToken(UniqueId, Token, "", false);
                        }
                    }
                    else
                    {
                        //Generate JWT Token
                        var Token = GenerateToken(UniqueId);

                        //Generate Refresh Token
                        var RefreshToken = CreateRefreshToken();

                        commonResponse = AddUpdateToken(UniqueId, Token, RefreshToken, true);
                        //loginResDTO.Token = response.Data.Token;
                        //loginResDTO.Token = response.Data.RefreshToken;
                        //commonResponse.Status = response.Status;

                    }

                    //commonResponse.Data.Token =
                    //    commonResponse.Data.RefreshToken = 
                    //commonResponse.Data = loginResDTO;
                    commonResponse.Data.UserDetail = UniqueId;
                    commonResponse.Message = "Login Successfully!";
                }
                else
                {
                    commonResponse.Message = "UserName or Password Is Wrong!";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        public bool ValidateToken(string token)
        {
            bool status = false;
            try
            {
                token = token?.Replace("Bearer ", "");
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = GetClaimsFromToken(token, out validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken != null && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    var UniqueId = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.PrimarySid).Select(_ => _.Value).FirstOrDefault();

                    int dataCount = 0;
                    var tokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"].ToString());
                    var refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"].ToString());
                    string FilePath = Path.Combine(_commonHelper.GetPhysicalRootPath(), "UserToken", "UserTokenData.json");
                    var userTockenDataModelList = new List<UserTockenDataModel>();
                    string jsonData = string.IsNullOrWhiteSpace(_commonHelper.ReadJsonFile(FilePath)) ? "[]" : _commonHelper.ReadJsonFile(FilePath);
                    userTockenDataModelList = JsonConvert.DeserializeObject<List<UserTockenDataModel>>(jsonData);
                    dataCount = userTockenDataModelList.Count();
                    UserTockenDataModel UserToken = userTockenDataModelList.FirstOrDefault(x => x.UserId == UniqueId);
                    if (UserToken != null)
                    {
                        if (UserToken.Token == token)
                        {
                            if (UserToken.RefreshTockenExpiredOn > _commonHelper.GetCurrentDateTime()) //Refresh Token Not Expired
                            {
                                if (UserToken.TockenExpiredOn > _commonHelper.GetCurrentDateTime()) //Token Not Expired
                                {
                                    status = true;
                                }
                                else
                                {
                                    string NewToken = GenerateToken(UniqueId);
                                    UserToken.Token = NewToken;
                                    UserToken.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                    UserToken.TockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(tokenExpiryMin);
                                    _commonHelper.AddJsonData(userTockenDataModelList, FilePath);

                                    status = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return status;
        }


        private string GenerateToken(string UniqueId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWTTokenSettings:IssuerSigningKey"]));
            string ValidIssuer = _iConfiguration["JWTTokenSettings:ValidIssuer"];
            string ValidAudience = _iConfiguration["JWTTokenSettings:ValidAudience"];
            int TokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"]);
            //int refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"]);

            var claims = new Claim[] { new Claim(ClaimTypes.PrimarySid, UniqueId) };
            var signingcredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

            var newJwtToken = new JwtSecurityToken(
                  issuer: ValidIssuer,
                  audience: ValidAudience,
                  notBefore: _commonHelper.GetCurrentDateTime(),
                  expires: _commonHelper.GetCurrentDateTime().AddMinutes(TokenExpiryMin),
                  signingCredentials: signingcredentials,
                  claims: claims
            ); ;

            var writetoken = tokenHandler.WriteToken(newJwtToken);

            return writetoken;
        }
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private CommonResponse AddUpdateToken(string UniqueId, string Token, string RefreshToken, bool IsRefreshTokenExpired)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                LoginResDTO loginResDTO = new LoginResDTO();
                commonResponse.Data = loginResDTO;
                //var tokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:TokenexpiryDays"].ToString());
                //var refreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());
                int dataCount = 0;

                var tokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"].ToString());
                var refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"].ToString());

                string FilePath = Path.Combine(_commonHelper.GetPhysicalRootPath(), "UserToken", "UserTokenData.json");

                var userTockenDataModelList = new List<UserTockenDataModel>();
                string jsonData = string.IsNullOrWhiteSpace(_commonHelper.ReadJsonFile(FilePath)) ? "[]" : _commonHelper.ReadJsonFile(FilePath);
                userTockenDataModelList = JsonConvert.DeserializeObject<List<UserTockenDataModel>>(jsonData);
                dataCount = userTockenDataModelList.Count();

                var UserToken = userTockenDataModelList.FirstOrDefault(x => x.UserId == UniqueId);
                UserTockenDataModel userTokenMst = new UserTockenDataModel();

                if (UserToken != null)
                {
                    userTokenMst = UserToken;
                    if (IsRefreshTokenExpired)
                    {
                        userTokenMst.Token = Token;
                        userTokenMst.RefreshToken = RefreshToken;
                        userTokenMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        userTokenMst.TockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(tokenExpiryMin);
                        userTokenMst.RefreshTockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(refreshTokenExpiryMin);
                    }
                    else
                    {
                        userTokenMst.Token = Token;
                        userTokenMst.TockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(tokenExpiryMin);
                        userTokenMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    }

                    var response = _commonHelper.AddJsonData(userTockenDataModelList, FilePath);
                    commonResponse.Status = response;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "User Token Details Updated Sucessfully!";
                    commonResponse.Data.Token = userTokenMst.Token;
                    commonResponse.Data.RefreshToken = userTokenMst.RefreshToken;
                }
                else
                {
                    userTokenMst.Id = dataCount + 1;
                    userTokenMst.UserId = UniqueId;
                    userTokenMst.Token = Token;
                    userTokenMst.RefreshToken = RefreshToken;
                    userTokenMst.IsActive = true;
                    userTokenMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    userTokenMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    userTokenMst.TockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(tokenExpiryMin);
                    userTokenMst.RefreshTockenExpiredOn = _commonHelper.GetCurrentDateTime().AddMinutes(refreshTokenExpiryMin);

                    userTockenDataModelList.Add(userTokenMst);
                    var response = _commonHelper.AddJsonData(userTockenDataModelList, FilePath);

                    commonResponse.Status = response;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "User Token Details Added Sucessfully!";
                    commonResponse.Data.Token = userTokenMst.Token;
                    commonResponse.Data.RefreshToken = userTokenMst.RefreshToken;
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        private ClaimsPrincipal GetClaimsFromToken(string token, out SecurityToken validatedToken)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();

            bool ValidateIssuerSigningKey = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateIssuerSigningKey"]);
            var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWTTokenSettings:IssuerSigningKey"]));

            bool ValidateIssuer = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateIssuer"]);
            string ValidIssuer = _iConfiguration["JWTTokenSettings:ValidIssuer"];

            bool ValidateAudience = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateAudience"]);
            string ValidAudience = _iConfiguration["JWTTokenSettings:ValidAudience"];

            bool ValidateLifetime = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateLifetime"]);

            bool TokenExpiryIsInDays = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:TokenExpiryIsInDays"]);
            bool RefreshTokenExpiryIsInDays = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:RefreshTokenExpiryIsInDays"]);

            //TimeSpan TokenExpiryTime = TokenExpiryIsInDays ? TimeSpan.FromDays(Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenexpiryDays"])) : TimeSpan.FromMinutes(Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"]));

            TimeSpan RefreshTokenExpiryTime = RefreshTokenExpiryIsInDays ? TimeSpan.FromDays(Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryDays"])) : TimeSpan.FromMinutes(Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"]));

            claimsPrincipal = tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuer = ValidateIssuer,
                ValidIssuer = ValidIssuer,
                ValidateAudience = ValidateAudience,
                ValidAudience = ValidAudience,
                ValidateIssuerSigningKey = ValidateIssuerSigningKey,
                IssuerSigningKey = IssuerSigningKey,
                ClockSkew = RefreshTokenExpiryTime,
                ValidateLifetime = ValidateLifetime
            }, out validatedToken);

            return claimsPrincipal;
        }


        //private TokenModel CreateJWT(dynamic UniqueId, bool IsRefreshTokenExpired, Guid? UniqueGuid = null)
        //{
        //    TokenModel model = new TokenModel();
        //    try
        //    {
        //        string UniqueIdStr = UniqueGuid != null ? UniqueGuid.ToString() : UniqueId.ToString();
        //        //string UniqueIdStr = Convert.ToString(UniqueId);
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWTTokenSettings:IssuerSigningKey"]));
        //        string ValidIssuer = _iConfiguration["JWTTokenSettings:ValidIssuer"];
        //        string ValidAudience = _iConfiguration["JWTTokenSettings:ValidAudience"];
        //        int TokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"]);
        //        int refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"]);

        //        var claims = new Claim[] { new Claim(ClaimTypes.PrimarySid, UniqueIdStr) };
        //        var signingcredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

        //        var newJwtToken = new JwtSecurityToken(
        //              issuer: ValidIssuer,
        //              audience: ValidAudience,
        //              notBefore: _commonHelper.GetCurrentDateTime(),
        //              expires: _commonHelper.GetCurrentDateTime().AddMinutes(TokenExpiryMin),
        //              signingCredentials: signingcredentials,
        //              claims: claims
        //        ); ;

        //        var writetoken = tokenHandler.WriteToken(newJwtToken);
        //        var RefreshToken = CreateRefreshToken();

        //        CommonResponse commonResponse = new CommonResponse();
        //        commonResponse.Data = new UserTockenDataModel();
        //        commonResponse.Data = AddUpdateToken(UniqueIdStr, writetoken, RefreshToken, IsRefreshTokenExpired);
        //        if (commonResponse.Status)
        //        {
        //            model.Token = commonResponse.Data.Token;                     // Writetoken;
        //            model.RefreshToken = commonResponse.Data.RefreshToken;       // RefreshToken;
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return model;
        //}


        //public TokenModel CreateJWT(dynamic UniqueId, bool IsRefreshTokenExpired, Guid? UniqueGuid = null)
        //{
        //    TokenModel model = new TokenModel();
        //    try
        //    {
        //        string UniqueIdStr = UniqueGuid != null ? UniqueGuid.ToString() : UniqueId.ToString();
        //        //string UniqueIdStr = Convert.ToString(UniqueId);
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWTTokenSettings:IssuerSigningKey"]));
        //        string ValidIssuer = _iConfiguration["JWTTokenSettings:ValidIssuer"];
        //        string ValidAudience = _iConfiguration["JWTTokenSettings:ValidAudience"];
        //        int TokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"]);
        //        int refreshTokenExpiryMin = Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"]);

        //        var claims = new Claim[] { new Claim(ClaimTypes.PrimarySid, UniqueIdStr) };
        //        var signingcredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

        //        var newJwtToken = new JwtSecurityToken(
        //              issuer: ValidIssuer,
        //              audience: ValidAudience,
        //              notBefore: _commonHelper.GetCurrentDateTime(),
        //              expires: _commonHelper.GetCurrentDateTime().AddMinutes(TokenExpiryMin),
        //              signingCredentials: signingcredentials,
        //              claims: claims
        //        ); ;

        //        var writetoken = tokenHandler.WriteToken(newJwtToken);
        //        var RefreshToken = CreateRefreshToken();

        //        CommonResponse commonResponse = new CommonResponse();
        //        commonResponse.Data = new UserTockenDataModel();
        //        commonResponse.Data = AddUpdateToken(UniqueIdStr, writetoken, RefreshToken, IsRefreshTokenExpired);
        //        if (commonResponse.Status)
        //        {
        //            model.Token = commonResponse.Data.Token;                     // Writetoken;
        //            model.RefreshToken = commonResponse.Data.RefreshToken;       // RefreshToken;
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return model;
        //}



        //public CommonResponse ValidateToken(TokenModel tokenModel)
        //{
        //    CommonResponse response = new CommonResponse();
        //    response.Message = "Refresh Token Not Generated.";
        //    try
        //    {
        //        SecurityToken validatedToken;
        //        ClaimsPrincipal claimsPrincipal = GetClaimsFromToken(tokenModel.Token, out validatedToken);

        //        var jwtToken = validatedToken as JwtSecurityToken;

        //        if (jwtToken != null && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
        //        {
        //            var UniqueId = claimsPrincipal.Claims.Where(_ => _.Type == ClaimTypes.PrimarySid).Select(_ => _.Value).FirstOrDefault();

        //            var UserTockenDetail = new CommonResponse();
        //            LoginResDTO logInResDTO = new LoginResDTO();
        //            if (UserTockenDetail.Status)
        //            {
        //                bool IsRefreshTokenExpired = false;
        //                if (UserTockenDetail.Data.ExpiredOn < _commonHelper.GetCurrentDateTime())
        //                {
        //                    IsRefreshTokenExpired = true;
        //                }
        //                var res = CreateJWT(UserTockenDetail.Data.UserId, IsRefreshTokenExpired);
        //                if (res != null)
        //                {
        //                    logInResDTO.Token = res.Token;
        //                    logInResDTO.RefreshToken = res.RefreshToken;

        //                    response.StatusCode = HttpStatusCode.OK;
        //                    response.Message = "Refresh Token Generated";
        //                    response.Data = logInResDTO;
        //                }
        //            }

        //        }
        //        else
        //        {
        //            response.Message = "Data not Found or token invalid";
        //            response.StatusCode = HttpStatusCode.ExpectationFailed;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //_helper.AddLog("Exception stack :: " + ex.ToString());
        //        response.StatusCode = HttpStatusCode.ExpectationFailed;
        //        response.Message = ex.Message;
        //        response.Data = ex;
        //    }
        //    return response;
        //}

        //private ClaimsPrincipal GetClaimsFromToken(string token, out SecurityToken validatedToken)
        //{
        //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    bool ValidateIssuerSigningKey = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateIssuerSigningKey"]);
        //    var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JWTTokenSettings:IssuerSigningKey"]));

        //    bool ValidateIssuer = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateIssuer"]);
        //    string ValidIssuer = _iConfiguration["JWTTokenSettings:ValidIssuer"];

        //    bool ValidateAudience = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateAudience"]);
        //    string ValidAudience = _iConfiguration["JWTTokenSettings:ValidAudience"];

        //    bool ValidateLifetime = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:ValidateLifetime"]);

        //    bool TokenExpiryIsInDays = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:TokenExpiryIsInDays"]);
        //    bool RefreshTokenExpiryIsInDays = Convert.ToBoolean(_iConfiguration["JWTTokenSettings:RefreshTokenExpiryIsInDays"]);

        //    //TimeSpan TokenExpiryTime = TokenExpiryIsInDays ? TimeSpan.FromDays(Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenexpiryDays"])) : TimeSpan.FromMinutes(Convert.ToInt32(_iConfiguration["JWTTokenSettings:TokenExpiryMinutes"]));

        //    TimeSpan RefreshTokenExpiryTime = RefreshTokenExpiryIsInDays ? TimeSpan.FromDays(Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryDays"])) : TimeSpan.FromMinutes(Convert.ToInt32(_iConfiguration["JWTTokenSettings:RefreshTokenexpiryMinutes"]));

        //    claimsPrincipal = tokenHandler.ValidateToken(token,
        //    new TokenValidationParameters
        //    {
        //        ValidateIssuer = ValidateIssuer,
        //        ValidIssuer = ValidIssuer,
        //        ValidateAudience = ValidateAudience,
        //        ValidAudience = ValidAudience,
        //        ValidateIssuerSigningKey = ValidateIssuerSigningKey,
        //        IssuerSigningKey = IssuerSigningKey,
        //        ClockSkew = RefreshTokenExpiryTime,
        //        ValidateLifetime = ValidateLifetime
        //    }, out validatedToken);

        //    return claimsPrincipal;
        //}


    }
}
