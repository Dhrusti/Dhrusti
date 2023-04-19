using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace MVC_Project.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IAuth _iAuth;
		private readonly IHttpContextAccessor _contextAccessor;
		public AuthenticationController(IAuth iAuth, IHttpContextAccessor contextAccessor)
		{
			_iAuth = iAuth;
			_contextAccessor = contextAccessor;
		}
		public IActionResult Index()
		{
			return View();
		}

		//public async Task<IActionResult> Login(LoginReqViewModel loginReqViewModel)
		//{
		//	CommonResponse commonResponse = new CommonResponse();
		//	commonResponse =  _iAuth.Login(loginReqViewModel.Adapt<LoginReqDTO>());
		//          if (commonResponse.Status)
		//          {
		//              return RedirectToAction("Dashboard", "Dashboard");
		//          }
		//          ViewBag.message = commonResponse.Message;
		//          return View("Login");

		//}

		public IActionResult Login(LoginReqViewModel loginReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			if (ModelState.IsValid)
			{
				commonResponse = _iAuth.Login(loginReqViewModel.Adapt<LoginReqDTO>());

				if (commonResponse.Status)
				{
					LoginResDTO loginResDTO = commonResponse.Data;
					commonResponse.Data = loginResDTO.Adapt<LoginResViewModel>();

					HttpContext.Session.SetObject("User", loginResDTO);
			 
					return RedirectToAction("Dashboard", "Dashboard", new { id = loginResDTO.Id });
				}
			}
			ViewBag.message = commonResponse.Message;
			return View();

		}
	}
}

