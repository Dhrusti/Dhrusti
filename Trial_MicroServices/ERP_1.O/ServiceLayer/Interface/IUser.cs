using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Implementation;

namespace ServiceLayer.Interface
{
	public interface IUser 
	{
		public CommonResponse GetAllUsers();
		public CommonResponse AddUsers(AddUserReqDTO addUserReqDTO);
		public CommonResponse GetUserById(GetAllUserReqDTO getAllUserReqDTO);

	}
}
