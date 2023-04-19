using DTO.ReqDTO;
using DTO.ResDTO;
using ERP_CRM.ViewModels.ReqViewModel;
using ERP_CRM.ViewModels.ResViewModel;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace ERP_CRM.Controllers
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
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetUserById")]
		public CommonResponse GetUserById(GetUserByIdReqViewModel getUserByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.GetUserById(getUserByIdReqViewModel.Adapt<GetUserByIdReqDTO>());
				GetUserByIdResDTO Model = commonResponse.Data;
				commonResponse.Data= Model;
			}
			catch(Exception)
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
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("UpdateUsers")]
		public CommonResponse UpdateUsers(UpdateUserReqViewModel updateUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.UpdateUsers(updateUserReqViewModel.Adapt<UpdateUserReqDTO>());
				UpdateUserResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<UpdateUserResViewModel>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("DeleteUsers")]
		public CommonResponse DeleteUsers(DeleteUserReqViewModel deleteUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iUser.DeleteUsers(deleteUserReqViewModel.Adapt<DeleteUserReqDTO>());
				DeleteUserResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<DeleteUserResViewModel>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}


	}
}
