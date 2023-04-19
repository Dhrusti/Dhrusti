using AuthByCookiesPOC.Helpers.CommonHelpers;
using AuthByCookiesPOC.Helpers.CommonModels;
using AuthByCookiesPOC.Models.ReqModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthByCookiesPOC.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Checks for user authorized or not
        /// </summary>
        /// <param name="request">UserName</param>
        /// <param name="request">Password</param>
        /// <param name="request">Remember Me</param>
        /// <returns>200 if success</returns>
        /// <returns>404 if not found</returns>
        /// <returns>500 if failed</returns>
        [HttpPost("Login")]
        public CommonResponse Login(LoginReq request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if ((request.UserName == "User" || request.UserName == "Admin") && request.Password == "123")
                {
                    if (Convert.ToBoolean(_configuration.GetSection("AuthenticationEnable").Value))
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Admin"), new Claim(ClaimTypes.Role, request.UserName) };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties();

                        props.IsPersistent = request.RememberMe;

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    }

                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = CommonConstants.Login_Successful;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = CommonConstants.Login_Failed;
                }
            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }

            return response;
        }

        /// <summary>
        /// Logout and clears the user session
        /// </summary>
        /// <returns>200 if success</returns>
        /// <returns>500 if failed</returns>
        [HttpPost("Logout")]
        public CommonResponse Logout()
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (Convert.ToBoolean(_configuration.GetSection("AuthenticationEnable").Value))
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                }

                response.Status = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = CommonConstants.Logout_Successfull;
            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }

            return response;
        }
    }
}
