using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using Mapster;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplAuthentication : IAuthentication
	{
		private readonly AuthenticationBLL _iAuthenticationBLL;

		public ImplAuthentication(AuthenticationBLL iAuthenticationBLL)
		{
			_iAuthenticationBLL = iAuthenticationBLL;
		}

		public CommonResponse Registration(RegistrationReqDTO registrationReqDTO)
		{
			return _iAuthenticationBLL.Registration(registrationReqDTO);
		}

		public CommonResponse Login(LoginReqDTO loginReqDTO)
		{
			return _iAuthenticationBLL.Login(loginReqDTO);
		}
	}
}
