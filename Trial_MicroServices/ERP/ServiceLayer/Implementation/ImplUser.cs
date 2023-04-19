using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplUser : IUser
	{
		private readonly UserBLL _iUserBLL;

		public ImplUser(UserBLL iUserBLL)
		{
			_iUserBLL = iUserBLL;
		}
		public CommonResponse GetAllUsers()
		{
			return _iUserBLL.GetAllUsers();
		}

		public CommonResponse AddUsers(AddUserReqDTO addUserReqDTO)
		{
			return _iUserBLL.AddUsers(addUserReqDTO);
		}

		public CommonResponse GetUserById(GetUserByIdReqDTO getUserByIdReqDTO)
		{
			return _iUserBLL.GetUserById(getUserByIdReqDTO);
		}

		public CommonResponse UpdateUsers(UpdateUserReqDTO updateUserReqDTO)
		{
			return _iUserBLL.UpdateUsers(updateUserReqDTO);
		}

		public CommonResponse DeleteUsers(DeleteUserReqDTO deleteUserReqDTO)
		{
			return _iUserBLL.DeleteUsers(deleteUserReqDTO);
		}

	}
}
