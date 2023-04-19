using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper.Models;

namespace ServiceLayer.Interface
{
	public interface IAuthentication
	{
		public CommonResponse Registration(RegistrationReqDTO registrationReqDTO);

		public CommonResponse Login(LoginReqDTO loginReqDTO);
	}
}
