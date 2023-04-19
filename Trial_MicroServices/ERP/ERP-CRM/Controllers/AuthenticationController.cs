using System.Net;
using DTO.ReqDTO;
using DTO.ResDTO;
using ERP_CRM.ViewModels.ReqViewModel;
using ERP_CRM.ViewModels.ResViewModel;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace ERP_CRM.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthentication _iAuthentication;

		public AuthenticationController(IAuthentication iAuthentication)
		{
			_iAuthentication = iAuthentication;
		}

        [AllowAnonymous]
		[HttpPost("Registration")]
		public CommonResponse Registration(RegistrationReqViewModel registrationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iAuthentication.Registration(registrationReqViewModel.Adapt<RegistrationReqDTO>());
				RegistrationResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<RegistrationResDTO>();
			}
			catch (Exception ex)
			{
				commonResponse.Status = false;
				commonResponse.StatusCode = HttpStatusCode.NotFound;
				commonResponse.Data = ex.ToString();
				commonResponse.Message = ex.Message;
			}
			return commonResponse;
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		public CommonResponse Login(LoginReqViewModel loginReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iAuthentication.Login(loginReqViewModel.Adapt<LoginReqDTO>());
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
