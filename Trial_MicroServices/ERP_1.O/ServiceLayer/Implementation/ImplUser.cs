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
		private readonly UserBLL _userBLL;
		public ImplUser(UserBLL userBLL)
		{
			_userBLL= userBLL;
		}
		public CommonResponse GetAllUsers()
		{
			return _userBLL.GetAllUsers();
		}

		public CommonResponse AddUsers(AddUserReqDTO addUserReqDTO)
		{
			return _userBLL.AddUsers(addUserReqDTO);
		}

		public CommonResponse GetUserById(GetAllUserReqDTO getAllUserReqDTO)
		{
			return _userBLL.GetUserById(getAllUserReqDTO);
		}

	}
}
