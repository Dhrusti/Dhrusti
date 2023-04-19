using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplAuthentication : IAuthentication
	{
		private readonly AuthenticationBLL _authenticationBLL;
		public ImplAuthentication(AuthenticationBLL authenticationBLL)
		{
			_authenticationBLL = authenticationBLL;
		}

		public CommonResponse Registration(RegistrationReqDTO registrationReqDTO)
		{
			return _authenticationBLL.Registration(registrationReqDTO);
		}

		public CommonResponse Login(LoginReqDTO loginReqDTO)
		{
			return _authenticationBLL.Login(loginReqDTO);
		}
	}
}
