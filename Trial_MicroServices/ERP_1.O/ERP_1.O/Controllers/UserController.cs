using DTO.ReqDTO;
using DTO.ResDTO;
using ERP_1.O.ViewModels.ReqViewModels;
using ERP_1.O.ViewModels.ResViewModels;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace ERP_1.O.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUser _iUser;
		public UserController(IUser iUser)
		{
			_iUser= iUser;
		}

		[HttpPost("GetAllUsers")]
		public CommonResponse GetAllUsers()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.GetAllUsers();
				List<GetAllUserResDTO> Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<List<GetAllUserResViewModel>>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetUserById")]
		public CommonResponse GetUserById(GetAllUserReqViewModel getAllUserReqView)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.GetUserById(getAllUserReqView.Adapt<GetAllUserReqDTO>());
				GetAllUserResDTO Model = commonResponse.Data;
				commonResponse.Data = Model;
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddUsers")]
		public CommonResponse AddUsers(AddUserReqViewModel addUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.AddUsers(addUserReqViewModel.Adapt<AddUserReqDTO>());
				AddUserResDTO model = commonResponse.Data;
				commonResponse.Data = model.Adapt<AddUserResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
	}
}
