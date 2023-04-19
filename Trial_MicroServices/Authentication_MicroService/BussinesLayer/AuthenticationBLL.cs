using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BussinesLayer
{
	public class AuthenticationBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly IConfiguration _configuration;
		private readonly AuthenticationMicroServiceDbContext _dbContext;
		public AuthenticationBLL(CommonRepo commonRepo, AuthenticationMicroServiceDbContext dbContext, IConfiguration configuration)
		{
			_commonRepo = commonRepo;
			_dbContext = dbContext;
			_configuration = configuration;
		}

		public CommonResponse Registration(RegistrationReqDTO registrationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			RegistrationResDTO registrationResDTO = new RegistrationResDTO();
			try
			{
				var user = _commonRepo.registrations().Where(x => x.Email == registrationReqDTO.Email.ToLower() && x.MobileNo == registrationReqDTO.MobileNo).FirstOrDefault();
				if (user == null)
				{

					RegistrationMst registrationMst = new RegistrationMst();
					registrationMst.FirstName = registrationReqDTO.FirstName;
					registrationMst.LastName = registrationReqDTO.LastName;
					registrationMst.Email = registrationReqDTO.Email;
					registrationMst.Password = registrationReqDTO.Password;
					registrationMst.MobileNo = registrationReqDTO.MobileNo;
					registrationMst.Designation = registrationReqDTO.Designation;
					registrationMst.Address = registrationReqDTO.Address;
					registrationMst.IsActive = true;
					registrationMst.IsDeleted = false;
					registrationMst.CreatedBy = 1;
					registrationMst.UpdatedBy = 1;
					registrationMst.CreatedDate = DateTime.Now;
					registrationMst.UpdatedDate = DateTime.Now;

					_dbContext.RegistrationMsts.Add(registrationMst);
					_dbContext.SaveChanges();

					registrationResDTO.RegistrationId = registrationMst.RegistrationId;
					registrationResDTO.FullName = registrationMst.FirstName + "" + registrationMst.LastName;
					registrationResDTO.Email = registrationMst.Email;

					commonResponse.Message = "User added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = registrationResDTO;
				}
				else
				{
					commonResponse.Message = "This account is already registered, try to login with different Email/MobileNo..";
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse Login(LoginReqDTO loginReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			LoginResDTO loginResDTO = new LoginResDTO();
			try
			{
				var loggedinUser = _commonRepo.registrations().FirstOrDefault(x => x.Email.ToLower() == loginReqDTO.Email.ToLower() && x.Password.ToLower() == loginReqDTO.Password.ToLower());
				if (loggedinUser != null)
				{
					TokenMst tokenMst = new TokenMst();
					string token = CreateToken2(loginReqDTO);

					var refreshToken = GenerateRefreshToken();
					//tokenMst.UserId = loginReqDTO.UserId;
					tokenMst.Token = token;
					tokenMst.RefreshToken = refreshToken.ToString();
					tokenMst.TokenCreated = DateTime.Now;
					tokenMst.TokenExpires = DateTime.Now.AddDays(7);

					_dbContext.TokenMsts.Add(tokenMst);
					_dbContext.SaveChanges();

					commonResponse.Data = tokenMst.Adapt<LoginResDTO>();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Invalid User.";
				}
			}
			catch { throw; }
			return commonResponse;
		}
		private string CreateToken2(LoginReqDTO loginReqDTO)
		{
			var secreatekey = this._configuration.GetSection("JwtSettings:JWTKey").Value;
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey));
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Role, "Admin")
			};

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddMinutes(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		private string GenerateRefreshToken()
		{

			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}


			//return refreshToken;
		}

	}
}
