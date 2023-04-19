using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer
{
	public class UserBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly Erp1OdbContext _dbContext;
		public UserBLL(CommonRepo commonRepo, Erp1OdbContext dbContext)
		{
			_commonRepo = commonRepo;
			_dbContext = dbContext;
		}

		public CommonResponse GetAllUsers()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var userList = _commonRepo.userList().ToList();
				if (userList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = userList.Adapt<List<GetAllUserResDTO>>();
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse GetUserById(GetAllUserReqDTO getAllUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var users = _commonRepo.userList().FirstOrDefault(x => x.Id == getAllUserReqDTO.Id);
				if (users != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}

				commonResponse.Data = users.Adapt<GetAllUserResDTO>();
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse AddUsers(AddUserReqDTO addUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddUserResDTO addUserResDTO = new AddUserResDTO();
			try
			{
				var user = _commonRepo.userList().Where(x => x.UserName == addUserReqDTO.UserName.ToLower() && x.Email == addUserReqDTO.Email.ToLower()).FirstOrDefault();
				if (user == null)
				{
					UserMst userMst = new UserMst();
					userMst.EmployeeCode = addUserReqDTO.EmployeeCode;
					userMst.FullName = addUserReqDTO.FullName;
					userMst.Gender = addUserReqDTO.Gender;
					userMst.Email = addUserReqDTO.Email;
					userMst.PhoneNumber = addUserReqDTO.PhoneNumber;
					userMst.EmergencyContact = addUserReqDTO.EmergencyContact;
					userMst.Dob = addUserReqDTO.Dob;
					userMst.UserName = addUserReqDTO.UserName;
					userMst.Password = addUserReqDTO.Password;
					userMst.ConfirmPassword = addUserReqDTO.ConfirmPassword;
					userMst.PermanentAddress = addUserReqDTO.PermanentAddress;
					userMst.CurrentAddress = addUserReqDTO.CurrentAddress;
					userMst.PostCode = addUserReqDTO.PostCode;
					userMst.EmploymentType = addUserReqDTO.EmploymentType;
					userMst.CompanyName = addUserReqDTO.CompanyName;
					userMst.Department = addUserReqDTO.Department;
					userMst.Designation = addUserReqDTO.Designation;
					userMst.Location = addUserReqDTO.Location;
					userMst.BloodGroup = addUserReqDTO.BloodGroup;
					userMst.OfferDate = addUserReqDTO.OfferDate;
					userMst.JoinDate = addUserReqDTO.JoinDate;
					userMst.Role = addUserReqDTO.Role;
					userMst.BankName = addUserReqDTO.BankName;
					userMst.AccountNumber = addUserReqDTO.AccountNumber;
					userMst.Branch = addUserReqDTO.Branch;
					userMst.Ifsccode = addUserReqDTO.Ifsccode;
					userMst.PfaccountNumber = addUserReqDTO.PfaccountNumber;
					userMst.PancardNumber = addUserReqDTO.PancardNumber;
					userMst.AdharCardNumber = addUserReqDTO.AdharCardNumber;
					userMst.Salary = addUserReqDTO.Salary;
					userMst.ReportingManager = addUserReqDTO.ReportingManager;
					userMst.Reason = addUserReqDTO.Reason;
					userMst.EmployeePersonalEmailId = addUserReqDTO.EmployeePersonalEmailId;
					userMst.ProbationPeriod = addUserReqDTO.ProbationPeriod;
					userMst.IsActive = true;
					userMst.IsDeleted = false;
					userMst.CreatedDate = DateTime.Now;
					userMst.UpdatedDate = DateTime.Now;

					_dbContext.UserMsts.Add(userMst);
					_dbContext.SaveChanges();

					addUserResDTO.EmployeeCode = userMst.EmployeeCode;
					addUserResDTO.FullName = userMst.FullName;
					addUserResDTO.Email = userMst.Email;
					addUserResDTO.PhoneNumber = userMst.PhoneNumber;
					addUserResDTO.Dob = userMst.Dob;
					addUserResDTO.UserName = userMst.UserName;

					commonResponse.Message = "User added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addUserResDTO;
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
