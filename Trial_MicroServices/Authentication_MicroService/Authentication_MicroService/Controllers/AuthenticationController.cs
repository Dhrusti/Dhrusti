using Helper.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using Authentication_MicroService.ViewModels.ReqViewModels;
using DTO.ReqDTO;
using Mapster;
using DTO.ResDTO;
using Authentication_MicroService.ViewModels.ResViewModels;

namespace Authentication_MicroService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthentication _iauthentication;
		public AuthenticationController(IAuthentication iauthentication)
		{
			_iauthentication = iauthentication;
		}

		[HttpPost("Registration")]
		public CommonResponse Registration(RegistrationReqViewModel registrationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iauthentication.Registration(registrationReqViewModel.Adapt<RegistrationReqDTO>());
				RegistrationResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<RegistrationResDTO>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("Login")]
		public CommonResponse Login(LoginReqViewModel loginReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iauthentication.Login(loginReqViewModel.Adapt<LoginReqDTO>());
				LoginResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<LoginResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
	}
}
