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
using Microsoft.Extensions.Configuration;

namespace BussinessLayer
{
	public class UserBLL
	{
		private readonly ErpDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly IConfiguration _config;

		public UserBLL(ErpDbContext dbContext, CommonRepo commonRepo, IConfiguration config)
		{
			_dbContext = dbContext;
			_commonRepo = commonRepo;
			_config = config;
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
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetUserById(GetUserByIdReqDTO getUserByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var users = _commonRepo.userList().FirstOrDefault(x => x.Id == getUserByIdReqDTO.Id);
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

				commonResponse.Data = users.Adapt<GetUserByIdResDTO>();
			}
			catch(Exception)
			{
				throw;
			}
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
					userMst.EmployeeCode= addUserReqDTO.EmployeeCode;
					userMst.FullName= addUserReqDTO.FullName;
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

					addUserResDTO.Id = userMst.Id;
					addUserResDTO.UserName = userMst.UserName;
					addUserResDTO.FullName = userMst.FullName;

					commonResponse.Message = "User added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addUserResDTO;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse UpdateUsers(UpdateUserReqDTO updateUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateUserResDTO updateUserResDTO = new UpdateUserResDTO();
			try
			{
				var user = _commonRepo.userList().Where(x => x.Id == updateUserReqDTO.Id && (x.UserName.ToLower() == updateUserReqDTO.UserName.ToLower())).ToList();
				if (user != null)
				{
					var userDetails = _commonRepo.userList().FirstOrDefault(x => x.Id == updateUserReqDTO.Id);
					if (userDetails != null)
					{
						UserMst userMst = userDetails;
						userMst.EmployeeCode = updateUserReqDTO.EmployeeCode;
						userMst.FullName = updateUserReqDTO.FullName;
						userMst.Gender = updateUserReqDTO.Gender;
						userMst.Email = updateUserReqDTO.Email;
						userMst.PhoneNumber = updateUserReqDTO.PhoneNumber;
						userMst.EmergencyContact = updateUserReqDTO.EmergencyContact;
						userMst.Dob = updateUserReqDTO.Dob;
						userMst.UserName = updateUserReqDTO.UserName;
						userMst.Password= updateUserReqDTO.Password;
						userMst.ConfirmPassword = updateUserReqDTO.ConfirmPassword;
						userMst.PermanentAddress = updateUserReqDTO.PermanentAddress;
						userMst.CurrentAddress = updateUserReqDTO.CurrentAddress;
						userMst.PostCode = updateUserReqDTO.PostCode;
						userMst.EmploymentType = updateUserReqDTO.EmploymentType;
						userMst.CompanyName = updateUserReqDTO.CompanyName;
						userMst.Department = updateUserReqDTO.Department;
						userMst.Designation = updateUserReqDTO.Designation;
						userMst.Location = updateUserReqDTO.Location;
						userMst.BloodGroup = updateUserReqDTO.BloodGroup;
						userMst.OfferDate = updateUserReqDTO.OfferDate;
						userMst.JoinDate = updateUserReqDTO.JoinDate;
						userMst.Role = updateUserReqDTO.Role;
						userMst.BankName = updateUserReqDTO.BankName;
						userMst.AccountNumber= updateUserReqDTO.AccountNumber;
						userMst.Branch = updateUserReqDTO.Branch;
						userMst.Ifsccode= updateUserReqDTO.Ifsccode;
						userMst.PfaccountNumber = updateUserReqDTO.PfaccountNumber;
						userMst.PancardNumber = updateUserReqDTO.PancardNumber;
						userMst.AdharCardNumber = updateUserReqDTO.AdharCardNumber;
						userMst.Salary = updateUserReqDTO.Salary;
						userMst.ReportingManager = updateUserReqDTO.ReportingManager;
						userMst.Reason = updateUserReqDTO.Reason;
						userMst.EmployeePersonalEmailId = updateUserReqDTO.EmployeePersonalEmailId;
						userMst.ProbationPeriod = updateUserReqDTO.ProbationPeriod;
						userMst.UpdatedBy = updateUserReqDTO.Id;
						userMst.CreatedBy = updateUserReqDTO.Id;
						userMst.CreatedDate = DateTime.Now;
						userMst.UpdatedDate = DateTime.Now;
						userMst.IsActive = true;
						userMst.IsDeleted = false;

						_dbContext.Entry(userMst).State = EntityState.Modified;
						_dbContext.SaveChanges();

						updateUserReqDTO.Id = userMst.Id;
						updateUserResDTO.UserName = userMst.UserName;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = updateUserResDTO;
						commonResponse.Message = "Successfully Updated.";
					}
					else
					{
						commonResponse.Status = false;
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "Can not find the user";
					}
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "User already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse DeleteUsers(DeleteUserReqDTO deleteUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			DeleteUserResDTO deleteUserResDTO = new DeleteUserResDTO();
			try
			{
				var users = _commonRepo.userList().FirstOrDefault(x => x.Id == deleteUserReqDTO.Id);
				if (users != null)
				{
					UserMst userMst = users;
					userMst.Id = deleteUserReqDTO.Id;
					userMst.UpdatedBy = deleteUserReqDTO.Id;
					userMst.IsDeleted = true;
					userMst.UpdatedDate = DateTime.Now;

					_dbContext.Entry(userMst).State = EntityState.Modified;
					_dbContext.SaveChanges();

					deleteUserResDTO.Id = userMst.Id;
					deleteUserResDTO.UserName = userMst.UserName;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = deleteUserResDTO;
					commonResponse.Message = "User Deleted Successfully.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Can not find the User.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

	}
}
