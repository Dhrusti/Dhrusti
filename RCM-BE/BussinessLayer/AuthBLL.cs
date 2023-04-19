using DataLayer;
using System.Net;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer
{
    public class AuthBLL
    {
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        private IHostingEnvironment _hostingEnvironment { get; }

        public AuthBLL(AuthRepo authRepo, IConfiguration iConfiguration, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment, CommonRepo commonRepo)
        {
            _authRepo = authRepo;
            _iConfiguration = iConfiguration;
            _commonHelper = commonHelper;
            _hostingEnvironment = hostingEnvironment;
            _commonRepo = commonRepo;
        }

        public CommonResponse Login(LoginReqDTO loginReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            LoginResDTO loginResDTO = new LoginResDTO();
            try
            {
				var result = _commonRepo.UserMstList().FirstOrDefault(x => (x.UserName == loginReqDTO.UserName) && x.IsDeleted == false && x.Password == loginReqDTO.Password.ToString());
				if (result != null)
				{
					loginResDTO.Id = result.Id;
					loginResDTO.FirstName = result.FirstName;
					loginResDTO.LastName = result.LastName;
					loginResDTO.UserName = result.UserName;

					commonResponse.Data = loginResDTO;
					commonResponse.Message = "Logged In Successfully...!!!";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
				}
				else
				{
					commonResponse.Message = "Incorrect Credentials...!!!";
				}
			}
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO)
        {
            CommonResponse response = new CommonResponse();
            //response = _authRepo.ValidateToken(refreshTokenReqDTO.Adapt<TokenModel>());
            return response;
        }
    }
}
