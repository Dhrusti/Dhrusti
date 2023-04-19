using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;

namespace ServiceLayer.Interface
{
	public interface IUser
	{
		public CommonResponse GetAllUsers();

		public CommonResponse GetUserById(GetUserByIdReqDTO getUserByIdReqDTO);

		public CommonResponse AddUsers(AddUserReqDTO addUserReqDTO);
		public CommonResponse UpdateUsers(UpdateUserReqDTO updateUserReqDTO);
		public CommonResponse DeleteUsers(DeleteUserReqDTO deleteUserReqDTO);
	}
}
