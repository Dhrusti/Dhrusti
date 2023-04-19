using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

		#region Login
		/// <summary>
		/// Check user credentials are valid or not.
		/// </summary>
		/// <param name="loginReqViewModel">Contains username and password</param>
		/// <returns>UserDetails, JWT tokens</returns>
		/// <response code="200">Login success</response>
		/// <response code="400">Login failed</response>
		[AllowAnonymous]
        [HttpPost("Login")]
        public CommonResponse Login(LoginReqViewModel loginReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _auth.Login(loginReqViewModel.Adapt<LoginReqDTO>());
                LoginResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<LoginResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

		#endregion

		//[AllowAnonymous]
		//[HttpPost("GetRefreshToken")]
		//public CommonResponse GetRefreshToken(RefreshTokenReqViewModel refreshTokenReqViewModel)
		//{
		//    CommonResponse commonResponse = new CommonResponse();
		//    try
		//    {
		//        commonResponse = _auth.GetRefreshToken(refreshTokenReqViewModel.Adapt<RefreshTokenReqDTO>());
		//        LoginResDTO Model = commonResponse.Data;
		//        commonResponse.Data = Model.Adapt<LoginResViewModel>();
		//    }
		//    catch (Exception) { throw; }
		//    return commonResponse;
		//}
	}
}
